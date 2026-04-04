namespace PcBuilderBackend.Application.Psus.Dtos
{
    public record UpdatePsuRequest(
        string Brand,
        string Model,
        int Wattage,
        string EfficiencyRating,
        string Modular,
        decimal Price);
}
