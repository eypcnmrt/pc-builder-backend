namespace PcBuilderBackend.Application.Motherboards.Dtos
{
    public record CreateMotherboardRequest(
        string Brand,
        string Model,
        string Socket,
        string Chipset,
        string FormFactor,
        int MaxRamGb,
        int RamSlots,
        string SupportedRamType,
        decimal Price);
}
