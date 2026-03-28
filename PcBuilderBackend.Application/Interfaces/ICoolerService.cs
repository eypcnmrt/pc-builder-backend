using Microsoft.AspNetCore.OData.Query;
using PcBuilderBackend.Application.Common;
using PcBuilderBackend.Application.Coolers.Dtos;
using PcBuilderBackend.Domain.Entities;

namespace PcBuilderBackend.Application.Interfaces
{
    public interface ICoolerService
    {
        Task<IResult<PagedData<Cooler>>> List(ODataQueryOptions<Cooler> options, int page, int pageSize, CancellationToken ct = default);
        Task<IResult<Cooler>> Get(int id, CancellationToken ct = default);
        Task<IResult<int>> Create(CreateCoolerRequest request, CancellationToken ct = default);
        Task<IResult> Update(int id, UpdateCoolerRequest request, CancellationToken ct = default);
        Task<IResult> Delete(int id, CancellationToken ct = default);
    }
}
