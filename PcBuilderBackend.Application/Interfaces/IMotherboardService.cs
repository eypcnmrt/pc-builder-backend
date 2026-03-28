using Microsoft.AspNetCore.OData.Query;
using PcBuilderBackend.Application.Common;
using PcBuilderBackend.Application.Motherboards.Dtos;
using PcBuilderBackend.Domain.Entities;

namespace PcBuilderBackend.Application.Interfaces
{
    public interface IMotherboardService
    {
        Task<IResult<PagedData<Motherboard>>> List(ODataQueryOptions<Motherboard> options, int page, int pageSize, CancellationToken ct = default);
        Task<IResult<PagedData<Motherboard>>> ListCompatible(string socket, int page, int pageSize, CancellationToken ct = default);
        Task<IResult<Motherboard>> Get(int id, CancellationToken ct = default);
        Task<IResult<int>> Create(CreateMotherboardRequest request, CancellationToken ct = default);
        Task<IResult> Update(int id, UpdateMotherboardRequest request, CancellationToken ct = default);
        Task<IResult> Delete(int id, CancellationToken ct = default);
    }
}
