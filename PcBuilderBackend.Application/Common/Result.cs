namespace PcBuilderBackend.Application.Common
{
    public class Result : IResult
    {
        public bool IsSuccess { get; private init; }
        public string Message { get; private init; } = string.Empty;
        public int StatusCode { get; private init; }

        public static Result Ok(string message = "") =>
            new() { IsSuccess = true, Message = message, StatusCode = 204 };

        public static Result NotFound(string message = "Kayıt bulunamadı.") =>
            new() { IsSuccess = false, Message = message, StatusCode = 404 };

        public static Result Error(string message = "Bir hata oluştu.") =>
            new() { IsSuccess = false, Message = message, StatusCode = 500 };
    }

    public class Result<T> : IResult<T>
    {
        public T? Data { get; private init; }
        public bool IsSuccess { get; private init; }
        public string Message { get; private init; } = string.Empty;
        public int StatusCode { get; private init; }

        public static Result<T> Ok(T data, string message = "") =>
            new() { Data = data, IsSuccess = true, Message = message, StatusCode = 200 };

        public static Result<T> Created(T data, string message = "") =>
            new() { Data = data, IsSuccess = true, Message = message, StatusCode = 201 };

        public static Result<T> NotFound(string message = "Kayıt bulunamadı.") =>
            new() { IsSuccess = false, Message = message, StatusCode = 404 };

        public static Result<T> Error(string message = "Bir hata oluştu.") =>
            new() { IsSuccess = false, Message = message, StatusCode = 500 };
    }
}
