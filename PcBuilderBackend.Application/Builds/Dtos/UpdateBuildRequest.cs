namespace PcBuilderBackend.Application.Builds.Dtos
{
    public class UpdateBuildRequest
    {
        public string? Name { get; set; }
        public int? ProcessorId { get; set; }
        public int? MotherboardId { get; set; }
        public int? GpuId { get; set; }
        public int? RamId { get; set; }
        public int? StorageId { get; set; }
        public int? PsuId { get; set; }
        public int? PcCaseId { get; set; }
        public int? CoolerId { get; set; }
    }
}
