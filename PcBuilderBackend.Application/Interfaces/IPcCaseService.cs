using Microsoft.AspNetCore.OData.Query;
using PcBuilderBackend.Application.Common;
using PcBuilderBackend.Application.PcCases.Dtos;
using PcBuilderBackend.Domain.Entities;

namespace PcBuilderBackend.Application.Interfaces
{
    public interface IPcCaseService
    {
        Task<IResult<PagedData<PcCase>>> List(ODataQueryOptions<PcCase> options, int page, int pageSize, CancellationToken ct = default);
        Task<IResult<PcCase>> Get(int id, CancellationToken ct = default);
        Task<IResult<int>> Create(CreatePcCaseRequest request, CancellationToken ct = default);
        Task<IResult> Update(int id, UpdatePcCaseRequest request, CancellationToken ct = default);
        Task<IResult> Delete(int id, CancellationToken ct = default);
    }
}
