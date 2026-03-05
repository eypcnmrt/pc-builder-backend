using Microsoft.AspNetCore.OData.Query;
using PcBuilderBackend.Application.Common;
using PcBuilderBackend.Application.Coolers.Dtos;
using PcBuilderBackend.Domain.Entities;

namespace PcBuilderBackend.Application.Interfaces
{
    public interface ICoolerService
    {
        Task<IResult<PagedData<Cooler>>> Listele(ODataQueryOptions<Cooler> options, int page, int pageSize, CancellationToken ct = default);
        Task<IResult<Cooler>> Getir(int id, CancellationToken ct = default);
        Task<IResult<int>> Ekle(CreateCoolerRequest request, CancellationToken ct = default);
        Task<IResult> Guncelle(int id, UpdateCoolerRequest request, CancellationToken ct = default);
        Task<IResult> Sil(int id, CancellationToken ct = default);
    }
}
