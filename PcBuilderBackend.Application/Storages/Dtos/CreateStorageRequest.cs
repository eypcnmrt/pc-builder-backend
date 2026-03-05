namespace PcBuilderBackend.Application.Storages.Dtos
{
    public record CreateStorageRequest(
        string Brand,
        string Model,
        string Type,
        int CapacityGb,
        string Interface,
        int ReadSpeedMbs,
        int WriteSpeedMbs);
}
