using Mapster;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PcBuilderBackend.Application.Common;
using PcBuilderBackend.Application.Gpus.Dtos;
using PcBuilderBackend.Application.Interfaces;
using PcBuilderBackend.Domain.Entities;
using PcBuilderBackend.Domain.Interfaces;

namespace PcBuilderBackend.Application.Services
{
    public class GpuService : IGpuService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GpuService> _logger;

        public GpuService(IUnitOfWork unitOfWork, ILogger<GpuService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IResult<PagedData<Gpu>>> List(ODataQueryOptions<Gpu> options, int page, int pageSize, CancellationToken ct = default)
        {
            try
            {
                var repo = _unitOfWork.GetRepository<Gpu>();
                var query = repo.AsQueryable().AsNoTracking();

                if (options.Filter != null)
                    query = (IQueryable<Gpu>)options.Filter.ApplyTo(query, new ODataQuerySettings());

                if (options.OrderBy != null)
                    query = options.OrderBy.ApplyTo(query, new ODataQuerySettings());

                var (items, totalCount) = await repo.GetPagedAsync(query, (page - 1) * pageSize, pageSize, ct);
                return Result<PagedData<Gpu>>.Ok(PagedData<Gpu>.Create(items, totalCount, page, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(List));
                return Result<PagedData<Gpu>>.Error("Bir hata oluştu.");
            }
        }

        public async Task<IResult<Gpu>> Get(int id, CancellationToken ct = default)
        {
            var entity = await _unitOfWork.GetRepository<Gpu>().GetByIdAsync(id, ct);
            if (entity is null)
                return Result<Gpu>.NotFound($"Ekran kartı {id} bulunamadı.");

            return Result<Gpu>.Ok(entity);
        }

        public async Task<IResult<int>> Create(CreateGpuRequest request, CancellationToken ct = default)
        {
            try
            {
                var repo = _unitOfWork.GetRepository<Gpu>();
                var entity = request.Adapt<Gpu>();
                await repo.AddAsync(entity, ct);
                await _unitOfWork.SaveChangesAsync(ct);
                return Result<int>.Created(entity.Id, "Ekran kartı başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(Create));
                return Result<int>.Error("Bir hata oluştu.");
            }
        }

        public async Task<IResult> Update(int id, UpdateGpuRequest request, CancellationToken ct = default)
        {
            try
            {
                var repo = _unitOfWork.GetRepository<Gpu>();
                var entity = await repo.GetByIdAsync(id, ct);
                if (entity is null)
                    return Result.NotFound($"Ekran kartı {id} bulunamadı.");

                request.Adapt(entity);
                repo.Update(entity);
                await _unitOfWork.SaveChangesAsync(ct);
                return Result.Ok("Ekran kartı başarıyla güncellendi.");
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
                var repo = _unitOfWork.GetRepository<Gpu>();
                var entity = await repo.GetByIdAsync(id, ct);
                if (entity is null)
                    return Result.NotFound($"Ekran kartı {id} bulunamadı.");

                repo.Delete(entity);
                await _unitOfWork.SaveChangesAsync(ct);
                return Result.Ok("Ekran kartı başarıyla silindi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(Delete));
                return Result.Error("Bir hata oluştu.");
            }
        }
    }
}
