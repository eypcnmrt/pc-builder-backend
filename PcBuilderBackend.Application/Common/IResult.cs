namespace PcBuilderBackend.Application.Common
{
    public interface IResult
    {
        bool IsSuccess { get; }
        string Message { get; }
        int StatusCode { get; }
    }

    public interface IResult<T> : IResult
    {
        T? Data { get; }
    }
}
