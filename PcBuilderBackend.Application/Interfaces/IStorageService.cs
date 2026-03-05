using Microsoft.AspNetCore.OData.Query;
using PcBuilderBackend.Application.Common;
using PcBuilderBackend.Application.Storages.Dtos;
using PcBuilderBackend.Domain.Entities;

namespace PcBuilderBackend.Application.Interfaces
{
    public interface IStorageService
    {
        Task<IResult<PagedData<Storage>>> Listele(ODataQueryOptions<Storage> options, int page, int pageSize, CancellationToken ct = default);
        Task<IResult<Storage>> Getir(int id, CancellationToken ct = default);
        Task<IResult<int>> Ekle(CreateStorageRequest request, CancellationToken ct = default);
        Task<IResult> Guncelle(int id, UpdateStorageRequest request, CancellationToken ct = default);
        Task<IResult> Sil(int id, CancellationToken ct = default);
    }
}
