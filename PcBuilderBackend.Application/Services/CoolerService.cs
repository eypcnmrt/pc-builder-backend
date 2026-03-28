using Mapster;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PcBuilderBackend.Application.Common;
using PcBuilderBackend.Application.Coolers.Dtos;
using PcBuilderBackend.Application.Interfaces;
using PcBuilderBackend.Domain.Entities;
using PcBuilderBackend.Domain.Interfaces;

namespace PcBuilderBackend.Application.Services
{
    public class CoolerService : ICoolerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CoolerService> _logger;

        public CoolerService(IUnitOfWork unitOfWork, ILogger<CoolerService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IResult<PagedData<Cooler>>> List(ODataQueryOptions<Cooler> options, int page, int pageSize, CancellationToken ct = default)
        {
            try
            {
                var repo = _unitOfWork.GetRepository<Cooler>();
                var query = repo.AsQueryable().AsNoTracking();

                if (options.Filter != null)
                    query = (IQueryable<Cooler>)options.Filter.ApplyTo(query, new ODataQuerySettings());
                if (options.OrderBy != null)
                    query = options.OrderBy.ApplyTo(query, new ODataQuerySettings());

                var (items, totalCount) = await repo.GetPagedAsync(query, (page - 1) * pageSize, pageSize, ct);
                return Result<PagedData<Cooler>>.Ok(PagedData<Cooler>.Create(items, totalCount, page, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(List));
                return Result<PagedData<Cooler>>.Error("Bir hata oluştu.");
            }
        }

        public async Task<IResult<Cooler>> Get(int id, CancellationToken ct = default)
        {
            var entity = await _unitOfWork.GetRepository<Cooler>().GetByIdAsync(id, ct);
            return entity is null
                ? Result<Cooler>.NotFound($"Soğutucu {id} bulunamadı.")
                : Result<Cooler>.Ok(entity);
        }

        public async Task<IResult<int>> Create(CreateCoolerRequest request, CancellationToken ct = default)
        {
            try
            {
                var repo = _unitOfWork.GetRepository<Cooler>();
                var entity = request.Adapt<Cooler>();
                await repo.AddAsync(entity, ct);
                await _unitOfWork.SaveChangesAsync(ct);
                return Result<int>.Created(entity.Id, "Soğutucu başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(Create));
                return Result<int>.Error("Bir hata oluştu.");
            }
        }

        public async Task<IResult> Update(int id, UpdateCoolerRequest request, CancellationToken ct = default)
        {
            try
            {
                var repo = _unitOfWork.GetRepository<Cooler>();
                var entity = await repo.GetByIdAsync(id, ct);
                if (entity is null) return Result.NotFound($"Soğutucu {id} bulunamadı.");
                request.Adapt(entity);
                repo.Update(entity);
                await _unitOfWork.SaveChangesAsync(ct);
                return Result.Ok("Soğutucu başarıyla güncellendi.");
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
                var repo = _unitOfWork.GetRepository<Cooler>();
                var entity = await repo.GetByIdAsync(id, ct);
                if (entity is null) return Result.NotFound($"Soğutucu {id} bulunamadı.");
                repo.Delete(entity);
                await _unitOfWork.SaveChangesAsync(ct);
                return Result.Ok("Soğutucu başarıyla silindi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(Delete));
                return Result.Error("Bir hata oluştu.");
            }
        }
    }
}
