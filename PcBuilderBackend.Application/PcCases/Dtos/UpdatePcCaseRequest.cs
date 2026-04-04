namespace PcBuilderBackend.Application.PcCases.Dtos
{
    public record UpdatePcCaseRequest(
        string Brand,
        string Model,
        string FormFactor,
        int MaxGpuLengthMm,
        int MaxCoolerHeightMm,
        decimal Price);
}
