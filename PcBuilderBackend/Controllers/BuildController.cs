using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PcBuilderBackend.Application.Builds.Dtos;
using PcBuilderBackend.Application.Interfaces;
using PcBuilderBackend.Extensions;

namespace PcBuilderBackend.Controllers
{
    [ApiController]
    [Tags("Build")]
    [Route("[controller]")]
    [Authorize]
    public class BuildController : ControllerBase
    {
        private readonly IBuildService _service;

        public BuildController(IBuildService service) => _service = service;

        private int GetUserId() =>
            int.Parse(User.FindFirstValue(JwtRegisteredClaimNames.Sub)
                ?? User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new UnauthorizedAccessException("Kullanıcı kimliği bulunamadı."));

        [HttpGet("current")]
        public async Task<IActionResult> GetCurrent(CancellationToken cancellationToken) =>
            (await _service.GetCurrent(GetUserId(), cancellationToken)).ToJsonResult();

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken) =>
            (await _service.GetById(id, GetUserId(), cancellationToken)).ToJsonResult();

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBuildRequest request, CancellationToken cancellationToken) =>
            (await _service.Create(request, GetUserId(), cancellationToken)).ToJsonResult();

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBuildRequest request, CancellationToken cancellationToken) =>
            (await _service.Update(id, request, GetUserId(), cancellationToken)).ToJsonResult();

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken) =>
            (await _service.Delete(id, GetUserId(), cancellationToken)).ToJsonResult();

        [HttpGet("{id:int}/activities")]
        public async Task<IActionResult> GetActivities(
            int id,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            CancellationToken cancellationToken = default) =>
            (await _service.GetActivities(id, GetUserId(), page, pageSize, cancellationToken)).ToJsonResult();
    }
}
