namespace PcBuilderBackend.Application.Coolers.Dtos
{
    public record CreateCoolerRequest(
        string Brand,
        string Model,
        string Type,
        int TdpW,
        string CompatibleSockets,
        int? HeightMm,
        int? RadiatorSizeMm);
}
