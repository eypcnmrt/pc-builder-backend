using Microsoft.AspNetCore.OData.Query;
using PcBuilderBackend.Application.Common;
using PcBuilderBackend.Application.Psus.Dtos;
using PcBuilderBackend.Domain.Entities;

namespace PcBuilderBackend.Application.Interfaces
{
    public interface IPsuService
    {
        Task<IResult<PagedData<Psu>>> Listele(ODataQueryOptions<Psu> options, int page, int pageSize, CancellationToken ct = default);
        Task<IResult<Psu>> Getir(int id, CancellationToken ct = default);
        Task<IResult<int>> Ekle(CreatePsuRequest request, CancellationToken ct = default);
        Task<IResult> Guncelle(int id, UpdatePsuRequest request, CancellationToken ct = default);
        Task<IResult> Sil(int id, CancellationToken ct = default);
    }
}
