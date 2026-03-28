using Microsoft.AspNetCore.OData.Query;
using PcBuilderBackend.Application.Common;
using PcBuilderBackend.Application.Storages.Dtos;
using PcBuilderBackend.Domain.Entities;

namespace PcBuilderBackend.Application.Interfaces
{
    public interface IStorageService
    {
        Task<IResult<PagedData<Storage>>> List(ODataQueryOptions<Storage> options, int page, int pageSize, CancellationToken ct = default);
        Task<IResult<Storage>> Get(int id, CancellationToken ct = default);
        Task<IResult<int>> Create(CreateStorageRequest request, CancellationToken ct = default);
        Task<IResult> Update(int id, UpdateStorageRequest request, CancellationToken ct = default);
        Task<IResult> Delete(int id, CancellationToken ct = default);
    }
}
