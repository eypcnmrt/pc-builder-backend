using Mapster;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PcBuilderBackend.Application.Common;
using PcBuilderBackend.Application.Interfaces;
using PcBuilderBackend.Application.Rams.Dtos;
using PcBuilderBackend.Domain.Entities;
using PcBuilderBackend.Domain.Interfaces;

namespace PcBuilderBackend.Application.Services
{
    public class RamService : IRamService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RamService> _logger;

        public RamService(IUnitOfWork unitOfWork, ILogger<RamService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IResult<PagedData<Ram>>> List(ODataQueryOptions<Ram> options, int page, int pageSize, CancellationToken ct = default)
        {
            try
            {
                var repo = _unitOfWork.GetRepository<Ram>();
                var query = repo.AsQueryable().AsNoTracking();

                if (options.Filter != null)
                    query = (IQueryable<Ram>)options.Filter.ApplyTo(query, new ODataQuerySettings());

                if (options.OrderBy != null)
                    query = options.OrderBy.ApplyTo(query, new ODataQuerySettings());

                var (items, totalCount) = await repo.GetPagedAsync(query, (page - 1) * pageSize, pageSize, ct);
                return Result<PagedData<Ram>>.Ok(PagedData<Ram>.Create(items, totalCount, page, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(List));
                return Result<PagedData<Ram>>.Error("Bir hata oluştu.");
            }
        }

        public async Task<IResult<Ram>> Get(int id, CancellationToken ct = default)
        {
            var entity = await _unitOfWork.GetRepository<Ram>().GetByIdAsync(id, ct);
            if (entity is null)
                return Result<Ram>.NotFound($"RAM {id} bulunamadı.");

            return Result<Ram>.Ok(entity);
        }

        public async Task<IResult<int>> Create(CreateRamRequest request, CancellationToken ct = default)
        {
            try
            {
                var repo = _unitOfWork.GetRepository<Ram>();
                var entity = request.Adapt<Ram>();
                await repo.AddAsync(entity, ct);
                await _unitOfWork.SaveChangesAsync(ct);
                return Result<int>.Created(entity.Id, "RAM başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(Create));
                return Result<int>.Error("Bir hata oluştu.");
            }
        }

        public async Task<IResult> Update(int id, UpdateRamRequest request, CancellationToken ct = default)
        {
            try
            {
                var repo = _unitOfWork.GetRepository<Ram>();
                var entity = await repo.GetByIdAsync(id, ct);
                if (entity is null)
                    return Result.NotFound($"RAM {id} bulunamadı.");

                request.Adapt(entity);
                repo.Update(entity);
                await _unitOfWork.SaveChangesAsync(ct);
                return Result.Ok("RAM başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(Update));
                return Result.Error("Bir hata oluştu.");
            }
        }

        public async Task<IResult> Delete(int id, CancellationToken ct = default)
        {
            try
            {
                var repo = _unitOfWork.GetRepository<Ram>();
                var entity = await repo.GetByIdAsync(id, ct);
                if (entity is null)
                    return Result.NotFound($"RAM {id} bulunamadı.");

                repo.Delete(entity);
                await _unitOfWork.SaveChangesAsync(ct);
                return Result.Ok("RAM başarıyla silindi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(Delete));
                return Result.Error("Bir hata oluştu.");
            }
        }
    }
}
