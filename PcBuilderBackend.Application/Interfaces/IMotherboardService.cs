using Microsoft.AspNetCore.OData.Query;
using PcBuilderBackend.Application.Common;
using PcBuilderBackend.Application.Motherboards.Dtos;
using PcBuilderBackend.Domain.Entities;

namespace PcBuilderBackend.Application.Interfaces
{
    public interface IMotherboardService
    {
        Task<IResult<PagedData<Motherboard>>> Listele(ODataQueryOptions<Motherboard> options, int page, int pageSize, CancellationToken ct = default);
        Task<IResult<PagedData<Motherboard>>> UyumluListele(string socket, int page, int pageSize, CancellationToken ct = default);
        Task<IResult<Motherboard>> Getir(int id, CancellationToken ct = default);
        Task<IResult<int>> Ekle(CreateMotherboardRequest request, CancellationToken ct = default);
        Task<IResult> Guncelle(int id, UpdateMotherboardRequest request, CancellationToken ct = default);
        Task<IResult> Sil(int id, CancellationToken ct = default);
    }
}
