using Microsoft.AspNetCore.Mvc;
using AppResult = PcBuilderBackend.Application.Common.IResult;

namespace PcBuilderBackend.Extensions
{
    public static class ResultExtensions
    {
        public static IActionResult ToJsonResult(this AppResult result) =>
            result.StatusCode switch
            {
                204 => new NoContentResult(),
                404 => new NotFoundObjectResult(new { result.Message }),
                _   => new ObjectResult(new { result.Message }) { StatusCode = result.StatusCode }
            };

        public static IActionResult ToJsonResult<T>(this PcBuilderBackend.Application.Common.IResult<T> result) =>
            result.StatusCode switch
            {
                200 => new OkObjectResult(result.Data),
                201 => new ObjectResult(result.Data) { StatusCode = 201 },
                404 => new NotFoundObjectResult(new { result.Message }),
                _   => new ObjectResult(new { result.Message }) { StatusCode = result.StatusCode }
            };
    }
}
