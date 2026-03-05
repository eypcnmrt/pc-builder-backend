using Microsoft.AspNetCore.OData.Query;
using PcBuilderBackend.Application.Common;
using PcBuilderBackend.Application.Processors.Dtos;
using PcBuilderBackend.Domain.Entities;

namespace PcBuilderBackend.Application.Interfaces
{
    public interface IProcessorService
    {
        Task<IResult<PagedData<Processor>>> Listele(ODataQueryOptions<Processor> options, int page, int pageSize, CancellationToken ct = default);
        Task<IResult<Processor>> Getir(int id, CancellationToken ct = default);
        Task<IResult<int>> Ekle(CreateProcessorRequest request, CancellationToken ct = default);
        Task<IResult> Guncelle(int id, UpdateProcessorRequest request, CancellationToken ct = default);
        Task<IResult> Sil(int id, CancellationToken ct = default);
    }
}
