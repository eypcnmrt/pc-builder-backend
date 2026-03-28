using Microsoft.AspNetCore.OData.Query;
using PcBuilderBackend.Application.Common;
using PcBuilderBackend.Application.Gpus.Dtos;
using PcBuilderBackend.Domain.Entities;

namespace PcBuilderBackend.Application.Interfaces
{
    public interface IGpuService
    {
        Task<IResult<PagedData<Gpu>>> List(ODataQueryOptions<Gpu> options, int page, int pageSize, CancellationToken ct = default);
        Task<IResult<Gpu>> Get(int id, CancellationToken ct = default);
        Task<IResult<int>> Create(CreateGpuRequest request, CancellationToken ct = default);
        Task<IResult> Update(int id, UpdateGpuRequest request, CancellationToken ct = default);
        Task<IResult> Delete(int id, CancellationToken ct = default);
    }
}
