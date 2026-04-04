namespace PcBuilderBackend.Application.Rams.Dtos
{
    public record UpdateRamRequest(
        string Brand,
        string Model,
        int CapacityGb,
        string Type,
        int SpeedMhz,
        int Modules,
        int LatencyCl,
        decimal Price);
}
