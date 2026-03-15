using PcBuilderBackend.Domain.Interfaces;

namespace PcBuilderBackend.Domain.Entities
{
    public class Build : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = "Yeni Yapılandırma";

        public int? ProcessorId { get; set; }
        public int? MotherboardId { get; set; }
        public int? GpuId { get; set; }
        public int? RamId { get; set; }
        public int? StorageId { get; set; }
        public int? PsuId { get; set; }
        public int? PcCaseId { get; set; }
        public int? CoolerId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public User? User { get; set; }
        public Processor? Processor { get; set; }
        public Motherboard? Motherboard { get; set; }
        public Gpu? Gpu { get; set; }
        public Ram? Ram { get; set; }
        public Storage? Storage { get; set; }
        public Psu? Psu { get; set; }
        public PcCase? PcCase { get; set; }
        public Cooler? Cooler { get; set; }

        public ICollection<BuildActivity> Activities { get; set; } = new List<BuildActivity>();
    }
}
