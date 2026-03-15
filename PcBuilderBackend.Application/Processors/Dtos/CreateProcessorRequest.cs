namespace PcBuilderBackend.Application.Processors.Dtos
{
    public record CreateProcessorRequest(
        string Brand,
        string Model,
        string Series,
        string Socket,
        int Cores,
        int Threads,
        double BaseClock,
        double BoostClock,
        int Tdp,
        int L3Cache,
        string MemoryType,
        bool IntegratedGraphics,
        decimal Price);
}
