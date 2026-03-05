namespace PcBuilderBackend.Application.Compatibility.Dtos
{
    public class CompatibilityCheckResult
    {
        public bool IsCompatible => Issues.Count == 0;
        public List<string> Issues { get; set; } = [];
    }
}
