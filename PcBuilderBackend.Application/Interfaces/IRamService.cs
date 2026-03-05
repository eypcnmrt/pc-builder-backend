using Microsoft.AspNetCore.OData.Query;
using PcBuilderBackend.Application.Common;
using PcBuilderBackend.Application.Rams.Dtos;
using PcBuilderBackend.Domain.Entities;

namespace PcBuilderBackend.Application.Interfaces
{
    public interface IRamService
    {
        Task<IResult<PagedData<Ram>>> Listele(ODataQueryOptions<Ram> options, int page, int pageSize, CancellationToken ct = default);
        Task<IResult<Ram>> Getir(int id, CancellationToken ct = default);
        Task<IResult<int>> Ekle(CreateRamRequest request, CancellationToken ct = default);
        Task<IResult> Guncelle(int id, UpdateRamRequest request, CancellationToken ct = default);
        Task<IResult> Sil(int id, CancellationToken ct = default);
    }
}
