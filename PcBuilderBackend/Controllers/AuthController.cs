using Microsoft.AspNetCore.Mvc;
using PcBuilderBackend.Application.Auth.Dtos;
using PcBuilderBackend.Application.Interfaces;
using PcBuilderBackend.Extensions;

namespace PcBuilderBackend.Controllers
{
    [ApiController]
    [Tags("Auth")]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service) => _service = service;

        [HttpPost("register")]
        public async Task<IActionResult> Kayit([FromBody] RegisterRequest request, CancellationToken cancellationToken) =>
            (await _service.Kayit(request, cancellationToken)).ToJsonResult();

        [HttpPost("login")]
        public async Task<IActionResult> Giris([FromBody] LoginRequest request, CancellationToken cancellationToken) =>
            (await _service.Giris(request, cancellationToken)).ToJsonResult();
    }
}
