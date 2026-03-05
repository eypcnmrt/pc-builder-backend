using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.OData.Edm;
using PcBuilderBackend.Application.Gpus.Dtos;
using PcBuilderBackend.Application.Interfaces;
using PcBuilderBackend.Domain.Entities;
using PcBuilderBackend.Extensions;

namespace PcBuilderBackend.Controllers
{
    [ApiController]
    [Tags("Ekran Kartı")]
    public class GpuController : ODataController
    {
        private readonly IGpuService _service;
        private readonly IEdmModel _edmModel;

        public GpuController(IGpuService service, IEdmModel edmModel)
        {
            _service = service;
            _edmModel = edmModel;
        }

        [HttpGet("/Gpu/OData")]
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
            var context = new ODataQueryContext(_edmModel, typeof(Gpu), null);
            var options = new ODataQueryOptions<Gpu>(context, HttpContext.Request);
            return (await _service.Listele(options, page, pageSize, cancellationToken)).ToJsonResult();
        }

        [HttpGet("/Gpu/{id:int}")]
        public async Task<IActionResult> Getir(int id, CancellationToken cancellationToken) =>
            (await _service.Getir(id, cancellationToken)).ToJsonResult();

        [HttpPost("/Gpu")]
        public async Task<IActionResult> Ekle([FromBody] CreateGpuRequest request, CancellationToken cancellationToken) =>
            (await _service.Ekle(request, cancellationToken)).ToJsonResult();

        [HttpPut("/Gpu/{id:int}")]
        public async Task<IActionResult> Guncelle(int id, [FromBody] UpdateGpuRequest request, CancellationToken cancellationToken) =>
            (await _service.Guncelle(id, request, cancellationToken)).ToJsonResult();

        [HttpDelete("/Gpu/{id:int}")]
        public async Task<IActionResult> Sil(int id, CancellationToken cancellationToken) =>
            (await _service.Sil(id, cancellationToken)).ToJsonResult();
    }
}
