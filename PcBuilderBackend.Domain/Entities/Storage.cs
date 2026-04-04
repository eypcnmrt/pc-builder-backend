using PcBuilderBackend.Domain.Interfaces;

namespace PcBuilderBackend.Domain.Entities
{
    public class Storage : IEntity
    {
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;       // SSD / HDD / NVMe
        public int CapacityGb { get; set; }
        public string Interface { get; set; } = string.Empty;  // SATA / PCIe 4.0
        public int ReadSpeedMbs { get; set; }
        public int WriteSpeedMbs { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}
