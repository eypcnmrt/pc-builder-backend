namespace PcBuilderBackend.Application.Processors.Dtos
{
    public record CreateProcessorRequest(
        string Brand,
        string Model,
        string Socket,
        int Cores,
        int Threads,
        double BaseClock,
        double BoostClock,
        int Tdp);
}
