using PcBuilderBackend.Domain.Interfaces;

namespace PcBuilderBackend.Domain.Entities
{
    public class Motherboard : IEntity
    {
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Socket { get; set; } = string.Empty;
        public string Chipset { get; set; } = string.Empty;
        public string FormFactor { get; set; } = string.Empty;
        public int MaxRamGb { get; set; }
        public int RamSlots { get; set; }
        public string SupportedRamType { get; set; } = string.Empty;   // DDR4 / DDR5
        public string? ImageUrl { get; set; }
    }
}
