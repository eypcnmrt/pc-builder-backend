namespace PcBuilderBackend.Application.Gpus.Dtos
{
    public record CreateGpuRequest(
        string Brand,
        string Model,
        int MemoryGb,
        int BoostClock,
        int Tdp,
        int LengthMm);
}
