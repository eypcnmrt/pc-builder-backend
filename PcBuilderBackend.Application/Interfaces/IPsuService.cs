using Microsoft.AspNetCore.OData.Query;
using PcBuilderBackend.Application.Common;
using PcBuilderBackend.Application.Psus.Dtos;
using PcBuilderBackend.Domain.Entities;

namespace PcBuilderBackend.Application.Interfaces
{
    public interface IPsuService
    {
        Task<IResult<PagedData<Psu>>> List(ODataQueryOptions<Psu> options, int page, int pageSize, CancellationToken ct = default);
        Task<IResult<Psu>> Get(int id, CancellationToken ct = default);
        Task<IResult<int>> Create(CreatePsuRequest request, CancellationToken ct = default);
        Task<IResult> Update(int id, UpdatePsuRequest request, CancellationToken ct = default);
        Task<IResult> Delete(int id, CancellationToken ct = default);
    }
}
