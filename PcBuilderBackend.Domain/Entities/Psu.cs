using PcBuilderBackend.Domain.Interfaces;

namespace PcBuilderBackend.Domain.Entities
{
    public class Psu : IEntity
    {
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Wattage { get; set; }
        public string EfficiencyRating { get; set; } = string.Empty; // 80+ Bronze/Silver/Gold/Platinum
        public string Modular { get; set; } = string.Empty;          // Full / Semi / Non
    }
}
