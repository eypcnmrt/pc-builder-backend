using PcBuilderBackend.Domain.Interfaces;

namespace PcBuilderBackend.Domain.Entities
{
    public class Ram : IEntity
    {
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int CapacityGb { get; set; }
        public string Type { get; set; } = string.Empty;
        public int SpeedMhz { get; set; }
        public int Modules { get; set; }
        public int LatencyCl { get; set; }
        public string? ImageUrl { get; set; }
    }
}
