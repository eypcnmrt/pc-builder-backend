using Microsoft.AspNetCore.Mvc;
using PcBuilderBackend.Application.Compatibility.Dtos;
using PcBuilderBackend.Application.Interfaces;
using PcBuilderBackend.Extensions;

namespace PcBuilderBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Tags("Uyumluluk")]
    public class CompatibilityController : ControllerBase
    {
        private readonly ICompatibilityService _service;

        public CompatibilityController(ICompatibilityService service) => _service = service;

        /// <summary>
        /// Seçilen bileşenlerin birbirleriyle uyumlu olup olmadığını kontrol eder.
        /// Tüm alanlar opsiyoneldir; yalnızca gönderdikleriniz kontrol edilir.
        /// </summary>
        [HttpPost("check")]
        public async Task<IActionResult> Check([FromBody] CompatibilityCheckRequest request, CancellationToken cancellationToken) =>
            (await _service.Check(request, cancellationToken)).ToJsonResult();
    }
}
