using PcBuilderBackend.Application.Builds.Dtos;
using PcBuilderBackend.Application.Common;
using PcBuilderBackend.Domain.Entities;

namespace PcBuilderBackend.Application.Interfaces
{
    public interface IBuildService
    {
        Task<IResult<Build>> GetCurrent(int userId, CancellationToken ct = default);
        Task<IResult<Build>> GetById(int id, int userId, CancellationToken ct = default);
        Task<IResult<int>> Create(CreateBuildRequest request, int userId, CancellationToken ct = default);
        Task<IResult<Build>> Update(int id, UpdateBuildRequest request, int userId, CancellationToken ct = default);
        Task<IResult> Delete(int id, int userId, CancellationToken ct = default);
        Task<IResult<PagedData<BuildActivityResponse>>> GetActivities(int buildId, int userId, int page, int pageSize, CancellationToken ct = default);
    }
}
