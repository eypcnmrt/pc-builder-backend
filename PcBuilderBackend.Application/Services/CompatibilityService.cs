using PcBuilderBackend.Application.Common;
using PcBuilderBackend.Application.Compatibility.Dtos;
using PcBuilderBackend.Application.Interfaces;
using PcBuilderBackend.Domain.Entities;
using PcBuilderBackend.Domain.Interfaces;

namespace PcBuilderBackend.Application.Services
{
    public class CompatibilityService : ICompatibilityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompatibilityService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<IResult<CompatibilityCheckResult>> Check(CompatibilityCheckRequest req, CancellationToken ct = default)
        {
            var result = new CompatibilityCheckResult();

            // Bileşenleri paralel yükle
            var processorTask   = LoadOrNull<Processor>(req.ProcessorId, ct);
            var motherboardTask = LoadOrNull<Motherboard>(req.MotherboardId, ct);
            var gpuTask         = LoadOrNull<Gpu>(req.GpuId, ct);
            var ramTask         = LoadOrNull<Ram>(req.RamId, ct);
            var psuTask         = LoadOrNull<Psu>(req.PsuId, ct);
            var caseTask        = LoadOrNull<PcCase>(req.PcCaseId, ct);
            var coolerTask      = LoadOrNull<Cooler>(req.CoolerId, ct);

            await Task.WhenAll(processorTask, motherboardTask, gpuTask, ramTask, psuTask, caseTask, coolerTask);

            var cpu       = processorTask.Result;
            var mb        = motherboardTask.Result;
            var gpu       = gpuTask.Result;
            var ram       = ramTask.Result;
            var psu       = psuTask.Result;
            var pcCase    = caseTask.Result;
            var cooler    = coolerTask.Result;

            // 1. CPU ↔ Anakart — Socket uyumu
            if (cpu != null && mb != null && cpu.Socket != mb.Socket)
                result.Issues.Add($"CPU soketi ({cpu.Socket}) Anakart soketiyle ({mb.Socket}) uyumsuz.");

            // 2. Anakart ↔ RAM — DDR tipi uyumu
            if (mb != null && ram != null && mb.SupportedRamType != ram.Type)
                result.Issues.Add($"Anakart RAM tipi ({mb.SupportedRamType}) ile RAM tipi ({ram.Type}) uyumsuz.");

            // 3. GPU ↔ Kasa — Fiziksel uzunluk
            if (gpu != null && pcCase != null && gpu.LengthMm > pcCase.MaxGpuLengthMm)
                result.Issues.Add($"GPU uzunluğu ({gpu.LengthMm}mm) Kasanın maksimum GPU uzunluğunu ({pcCase.MaxGpuLengthMm}mm) aşıyor.");

            // 4. Soğutucu ↔ Kasa — Hava soğutucusu yüksekliği
            if (cooler != null && pcCase != null && cooler.Type == "Air" && cooler.HeightMm.HasValue
                && cooler.HeightMm.Value > pcCase.MaxCoolerHeightMm)
                result.Issues.Add($"Soğutucu yüksekliği ({cooler.HeightMm}mm) Kasanın maksimum soğutucu yüksekliğini ({pcCase.MaxCoolerHeightMm}mm) aşıyor.");

            // 5. Soğutucu ↔ CPU — Socket desteği
            if (cooler != null && cpu != null)
            {
                var supported = cooler.CompatibleSockets
                    .Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                if (!supported.Contains(cpu.Socket))
                    result.Issues.Add($"Soğutucu ({cooler.Brand} {cooler.Model}) CPU soketini ({cpu.Socket}) desteklemiyor. Desteklenenler: {cooler.CompatibleSockets}");
            }

            // 6. Anakart ↔ Kasa — Form faktör uyumu
            if (mb != null && pcCase != null && !IsFormFactorCompatible(pcCase.FormFactor, mb.FormFactor))
                result.Issues.Add($"Anakart form faktörü ({mb.FormFactor}) Kasa form faktörüyle ({pcCase.FormFactor}) uyumsuz.");

            // 7. PSU — Güç yeterliliği (CPU + GPU TDP'si baz alınır, %20 tampon eklenir)
            if (psu != null)
            {
                int totalTdp = (cpu?.Tdp ?? 0) + (gpu?.Tdp ?? 0);
                int recommended = (int)(totalTdp * 1.2);
                if (psu.Wattage < recommended)
                    result.Issues.Add($"PSU gücü ({psu.Wattage}W) yetersiz. CPU+GPU TDP toplamı {totalTdp}W, önerilen minimum: {recommended}W.");
            }

            return Result<CompatibilityCheckResult>.Ok(result);
        }

        // ATX kasa: ATX/mATX/ITX anakart alır
        // mATX kasa: mATX/ITX anakart alır
        // ITX kasa: sadece ITX anakart alır
        private static bool IsFormFactorCompatible(string caseFormFactor, string mbFormFactor) =>
            caseFormFactor switch
            {
                "ATX"  => true,
                "mATX" => mbFormFactor is "mATX" or "ITX",
                "ITX"  => mbFormFactor == "ITX",
                _      => false
            };

        private async Task<T?> LoadOrNull<T>(int? id, CancellationToken ct) where T : class, IEntity
        {
            if (id is null) return null;
            return await _unitOfWork.GetRepository<T>().GetByIdAsync(id.Value, ct);
        }
    }
}
