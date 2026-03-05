namespace PcBuilderBackend.Application.Storages.Dtos
{
    public record UpdateStorageRequest(
        string Brand,
        string Model,
        string Type,
        int CapacityGb,
        string Interface,
        int ReadSpeedMbs,
        int WriteSpeedMbs);
}
