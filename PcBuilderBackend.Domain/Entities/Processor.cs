using PcBuilderBackend.Domain.Interfaces;

namespace PcBuilderBackend.Domain.Entities
{
    public class Processor : IEntity
    {
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Series { get; set; } = string.Empty;
        public string Architecture { get; set; } = string.Empty;
        public string Socket { get; set; } = string.Empty;
        public int Cores { get; set; }
        public int Threads { get; set; }
        public double BaseClock { get; set; }
        public double BoostClock { get; set; }
        public int Tdp { get; set; }
        public int L3Cache { get; set; }
        public string MemoryType { get; set; } = string.Empty;
        public bool IntegratedGraphics { get; set; }
        public decimal Price { get; set; }
    }
}
