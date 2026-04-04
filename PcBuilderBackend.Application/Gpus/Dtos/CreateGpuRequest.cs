namespace PcBuilderBackend.Application.Gpus.Dtos
{
    public record CreateGpuRequest(
        string Brand,
        string Model,
        int MemoryGb,
        string MemoryType,
        int CoreClock,
        int BoostClock,
        int Tdp,
        int LengthMm,
        decimal Price);
}
