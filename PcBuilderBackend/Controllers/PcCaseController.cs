using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.OData.Edm;
using PcBuilderBackend.Application.Interfaces;
using PcBuilderBackend.Application.PcCases.Dtos;
using PcBuilderBackend.Domain.Entities;
using PcBuilderBackend.Extensions;

namespace PcBuilderBackend.Controllers
{
    [ApiController]
    [Tags("Kasa")]
    public class PcCaseController : ODataController
    {
        private readonly IPcCaseService _service;
        private readonly IEdmModel _edmModel;

        public PcCaseController(IPcCaseService service, IEdmModel edmModel)
        {
            _service = service;
            _edmModel = edmModel;
        }

        [HttpGet("/PcCase/OData")]
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
            var context = new ODataQueryContext(_edmModel, typeof(PcCase), null);
            var options = new ODataQueryOptions<PcCase>(context, HttpContext.Request);
            return (await _service.Listele(options, page, pageSize, cancellationToken)).ToJsonResult();
        }

        [HttpGet("/PcCase/{id:int}")]
        public async Task<IActionResult> Getir(int id, CancellationToken cancellationToken) =>
            (await _service.Getir(id, cancellationToken)).ToJsonResult();

        [HttpPost("/PcCase")]
        public async Task<IActionResult> Ekle([FromBody] CreatePcCaseRequest request, CancellationToken cancellationToken) =>
            (await _service.Ekle(request, cancellationToken)).ToJsonResult();

        [HttpPut("/PcCase/{id:int}")]
        public async Task<IActionResult> Guncelle(int id, [FromBody] UpdatePcCaseRequest request, CancellationToken cancellationToken) =>
            (await _service.Guncelle(id, request, cancellationToken)).ToJsonResult();

        [HttpDelete("/PcCase/{id:int}")]
        public async Task<IActionResult> Sil(int id, CancellationToken cancellationToken) =>
            (await _service.Sil(id, cancellationToken)).ToJsonResult();
    }
}
