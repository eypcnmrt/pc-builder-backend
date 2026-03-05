namespace PcBuilderBackend.Application.Compatibility.Dtos
{
    public record CompatibilityCheckRequest(
        int? ProcessorId,
        int? MotherboardId,
        int? GpuId,
        int? RamId,
        int? StorageId,
        int? PsuId,
        int? PcCaseId,
        int? CoolerId);
}
