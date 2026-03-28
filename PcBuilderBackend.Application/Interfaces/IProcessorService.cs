using Microsoft.AspNetCore.OData.Query;
using PcBuilderBackend.Application.Common;
using PcBuilderBackend.Application.Processors.Dtos;
using PcBuilderBackend.Domain.Entities;

namespace PcBuilderBackend.Application.Interfaces
{
    public interface IProcessorService
    {
        Task<IResult<PagedData<Processor>>> List(ODataQueryOptions<Processor> options, int page, int pageSize, CancellationToken ct = default);
        Task<IResult<Processor>> Get(int id, CancellationToken ct = default);
        Task<IResult<int>> Create(CreateProcessorRequest request, CancellationToken ct = default);
        Task<IResult> Update(int id, UpdateProcessorRequest request, CancellationToken ct = default);
        Task<IResult> Delete(int id, CancellationToken ct = default);
    }
}
