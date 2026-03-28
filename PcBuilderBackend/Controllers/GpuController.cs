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
        public async Task<IActionResult> List(
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
            return (await _service.List(options, page, pageSize, cancellationToken)).ToJsonResult();
        }

        [HttpGet("/Gpu/{id:int}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken) =>
            (await _service.Get(id, cancellationToken)).ToJsonResult();

        [HttpPost("/Gpu")]
        public async Task<IActionResult> Create([FromBody] CreateGpuRequest request, CancellationToken cancellationToken) =>
            (await _service.Create(request, cancellationToken)).ToJsonResult();

        [HttpPut("/Gpu/{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateGpuRequest request, CancellationToken cancellationToken) =>
            (await _service.Update(id, request, cancellationToken)).ToJsonResult();

        [HttpDelete("/Gpu/{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken) =>
            (await _service.Delete(id, cancellationToken)).ToJsonResult();
    }
}
