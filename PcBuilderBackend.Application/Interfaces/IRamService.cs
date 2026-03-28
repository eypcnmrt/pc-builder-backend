using Microsoft.AspNetCore.OData.Query;
using PcBuilderBackend.Application.Common;
using PcBuilderBackend.Application.Rams.Dtos;
using PcBuilderBackend.Domain.Entities;

namespace PcBuilderBackend.Application.Interfaces
{
    public interface IRamService
    {
        Task<IResult<PagedData<Ram>>> List(ODataQueryOptions<Ram> options, int page, int pageSize, CancellationToken ct = default);
        Task<IResult<Ram>> Get(int id, CancellationToken ct = default);
        Task<IResult<int>> Create(CreateRamRequest request, CancellationToken ct = default);
        Task<IResult> Update(int id, UpdateRamRequest request, CancellationToken ct = default);
        Task<IResult> Delete(int id, CancellationToken ct = default);
    }
}
