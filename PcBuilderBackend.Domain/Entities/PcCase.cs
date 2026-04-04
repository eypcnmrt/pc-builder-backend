using PcBuilderBackend.Domain.Interfaces;

namespace PcBuilderBackend.Domain.Entities
{
    public class PcCase : IEntity
    {
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string FormFactor { get; set; } = string.Empty;  // ATX / mATX / ITX
        public int MaxGpuLengthMm { get; set; }
        public int MaxCoolerHeightMm { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}
