using PcBuilderBackend.Domain.Entities;

namespace PcBuilderBackend.Application.Builds.Dtos
{
    public class BuildResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Processor? Processor { get; set; }
        public Motherboard? Motherboard { get; set; }
        public Gpu? Gpu { get; set; }
        public Ram? Ram { get; set; }
        public Storage? Storage { get; set; }
        public Psu? Psu { get; set; }
        public PcCase? PcCase { get; set; }
        public Cooler? Cooler { get; set; }

        public decimal TotalPrice =>
            (Processor?.Price ?? 0) +
            (Motherboard?.Price ?? 0) +
            (Gpu?.Price ?? 0) +
            (Ram?.Price ?? 0) +
            (Storage?.Price ?? 0) +
            (Psu?.Price ?? 0) +
            (PcCase?.Price ?? 0) +
            (Cooler?.Price ?? 0);

        public static BuildResponse FromEntity(Build build) => new()
        {
            Id = build.Id,
            Name = build.Name,
            CreatedAt = build.CreatedAt,
            UpdatedAt = build.UpdatedAt,
            Processor = build.Processor,
            Motherboard = build.Motherboard,
            Gpu = build.Gpu,
            Ram = build.Ram,
            Storage = build.Storage,
            Psu = build.Psu,
            PcCase = build.PcCase,
            Cooler = build.Cooler,
        };
    }
}
