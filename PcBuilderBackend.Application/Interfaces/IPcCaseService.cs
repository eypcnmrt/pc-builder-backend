using Microsoft.AspNetCore.OData.Query;
using PcBuilderBackend.Application.Common;
using PcBuilderBackend.Application.PcCases.Dtos;
using PcBuilderBackend.Domain.Entities;

namespace PcBuilderBackend.Application.Interfaces
{
    public interface IPcCaseService
    {
        Task<IResult<PagedData<PcCase>>> Listele(ODataQueryOptions<PcCase> options, int page, int pageSize, CancellationToken ct = default);
        Task<IResult<PcCase>> Getir(int id, CancellationToken ct = default);
        Task<IResult<int>> Ekle(CreatePcCaseRequest request, CancellationToken ct = default);
        Task<IResult> Guncelle(int id, UpdatePcCaseRequest request, CancellationToken ct = default);
        Task<IResult> Sil(int id, CancellationToken ct = default);
    }
}
