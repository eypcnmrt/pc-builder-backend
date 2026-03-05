using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.OData.Edm;
using PcBuilderBackend.Application.Interfaces;
using PcBuilderBackend.Application.Motherboards.Dtos;
using PcBuilderBackend.Domain.Entities;
using PcBuilderBackend.Extensions;

namespace PcBuilderBackend.Controllers
{
    [ApiController]
    [Tags("Anakart")]
    public class MotherboardController : ODataController
    {
        private readonly IMotherboardService _service;
        private readonly IEdmModel _edmModel;

        public MotherboardController(IMotherboardService service, IEdmModel edmModel)
        {
            _service = service;
            _edmModel = edmModel;
        }

        [HttpGet("/Motherboard/OData")]
        public async Task<IActionResult> Listele(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery(Name = "$filter")] string? odataFilter = null,
            [FromQuery(Name = "$orderby")] string? odataOrderby = null,
            [FromQuery(Name = "$top")] int? odataTop = null,
            [FromQuery(Name = "$skip")] int? odataSkip = null,
            [FromQuery(Name = "$count")] bool? odataCount = null,
            CancellationToken cancellationToken = default)
        {
            var context = new ODataQueryContext(_edmModel, typeof(Motherboard), null);
            var options = new ODataQueryOptions<Motherboard>(context, HttpContext.Request);
            return (await _service.Listele(options, page, pageSize, cancellationToken)).ToJsonResult();
        }

        [HttpGet("/Motherboard/compatible")]
        public async Task<IActionResult> UyumluListele([FromQuery] string socket, [FromQuery] int page = 1, [FromQuery] int pageSize = 10, CancellationToken cancellationToken = default) =>
            (await _service.UyumluListele(socket, page, pageSize, cancellationToken)).ToJsonResult();

        [HttpGet("/Motherboard/{id:int}")]
        public async Task<IActionResult> Getir(int id, CancellationToken cancellationToken) =>
            (await _service.Getir(id, cancellationToken)).ToJsonResult();

        [HttpPost("/Motherboard")]
        public async Task<IActionResult> Ekle([FromBody] CreateMotherboardRequest request, CancellationToken cancellationToken) =>
            (await _service.Ekle(request, cancellationToken)).ToJsonResult();

        [HttpPut("/Motherboard/{id:int}")]
        public async Task<IActionResult> Guncelle(int id, [FromBody] UpdateMotherboardRequest request, CancellationToken cancellationToken) =>
            (await _service.Guncelle(id, request, cancellationToken)).ToJsonResult();

        [HttpDelete("/Motherboard/{id:int}")]
        public async Task<IActionResult> Sil(int id, CancellationToken cancellationToken) =>
            (await _service.Sil(id, cancellationToken)).ToJsonResult();
    }
}
