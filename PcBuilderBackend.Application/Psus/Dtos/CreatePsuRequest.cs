namespace PcBuilderBackend.Application.Psus.Dtos
{
    public record CreatePsuRequest(
        string Brand,
        string Model,
        int Wattage,
        string EfficiencyRating,
        string Modular,
        decimal Price);
}
