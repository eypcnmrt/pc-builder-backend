using PcBuilderBackend.Domain.Interfaces;

namespace PcBuilderBackend.Domain.Entities
{
    public class Cooler : IEntity
    {
        public int Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;               // Air / Liquid
        public int TdpW { get; set; }
        public string CompatibleSockets { get; set; } = string.Empty;  // "LGA1700,AM5,AM4"
        public int? HeightMm { get; set; }                             // Hava soğutucusu yüksekliği
        public int? RadiatorSizeMm { get; set; }                       // Sıvı soğutucusu radyatör boyutu
        public string? ImageUrl { get; set; }
    }
}
