using Microsoft.AspNetCore.OData.Query;
using PcBuilderBackend.Application.Common;
using PcBuilderBackend.Application.Gpus.Dtos;
using PcBuilderBackend.Domain.Entities;

namespace PcBuilderBackend.Application.Interfaces
{
    public interface IGpuService
    {
        Task<IResult<PagedData<Gpu>>> Listele(ODataQueryOptions<Gpu> options, int page, int pageSize, CancellationToken ct = default);
        Task<IResult<Gpu>> Getir(int id, CancellationToken ct = default);
        Task<IResult<int>> Ekle(CreateGpuRequest request, CancellationToken ct = default);
        Task<IResult> Guncelle(int id, UpdateGpuRequest request, CancellationToken ct = default);
        Task<IResult> Sil(int id, CancellationToken ct = default);
    }
}
