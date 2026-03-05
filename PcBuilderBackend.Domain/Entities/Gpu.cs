using PcBuilderBackend.Domain.Interfaces;

namespace PcBuilderBackend.Domain.Entities
{
    public class Gpu : IEntity
    {
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int MemoryGb { get; set; }
        public string MemoryType { get; set; } = string.Empty;
        public int CoreClock { get; set; }
        public int BoostClock { get; set; }
        public int Tdp { get; set; }
        public int LengthMm { get; set; }
    }
}
