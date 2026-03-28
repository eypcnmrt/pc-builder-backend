using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.OData.Edm;
using PcBuilderBackend.Application.Interfaces;
using PcBuilderBackend.Application.Rams.Dtos;
using PcBuilderBackend.Domain.Entities;
using PcBuilderBackend.Extensions;

namespace PcBuilderBackend.Controllers
{
    [ApiController]
    [Tags("RAM")]
    public class RamController : ODataController
    {
        private readonly IRamService _service;
        private readonly IEdmModel _edmModel;

        public RamController(IRamService service, IEdmModel edmModel)
        {
            _service = service;
            _edmModel = edmModel;
        }

        [HttpGet("/Ram/OData")]
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
            var context = new ODataQueryContext(_edmModel, typeof(Ram), null);
            var options = new ODataQueryOptions<Ram>(context, HttpContext.Request);
            return (await _service.List(options, page, pageSize, cancellationToken)).ToJsonResult();
        }

        [HttpGet("/Ram/{id:int}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken) =>
            (await _service.Get(id, cancellationToken)).ToJsonResult();

        [HttpPost("/Ram")]
        public async Task<IActionResult> Create([FromBody] CreateRamRequest request, CancellationToken cancellationToken) =>
            (await _service.Create(request, cancellationToken)).ToJsonResult();

        [HttpPut("/Ram/{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRamRequest request, CancellationToken cancellationToken) =>
            (await _service.Update(id, request, cancellationToken)).ToJsonResult();

        [HttpDelete("/Ram/{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken) =>
            (await _service.Delete(id, cancellationToken)).ToJsonResult();
    }
}
