namespace PcBuilderBackend.Application.Builds.Dtos
{
    public class BuildActivityResponse
    {
        public int Id { get; set; }
        public int BuildId { get; set; }
        public string ComponentType { get; set; } = string.Empty;
        public int? ComponentId { get; set; }
        public string Action { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        // Frontend-friendly aliases
        public DateTime Timestamp => CreatedAt;
        public string Detail => Description;
    }
}
