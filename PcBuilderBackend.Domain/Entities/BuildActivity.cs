using PcBuilderBackend.Domain.Interfaces;

namespace PcBuilderBackend.Domain.Entities
{
    public class BuildActivity : IEntity
    {
        public int Id { get; set; }
        public int BuildId { get; set; }
        public string ComponentType { get; set; } = string.Empty;  // Processor, Gpu, Ram, vb.
        public int? ComponentId { get; set; }
        public string Action { get; set; } = string.Empty;         // Added, Removed, Updated
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public Build? Build { get; set; }
    }
}
