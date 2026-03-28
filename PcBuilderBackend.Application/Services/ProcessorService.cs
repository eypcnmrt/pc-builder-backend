using Mapster;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PcBuilderBackend.Application.Common;
using PcBuilderBackend.Application.Interfaces;
using PcBuilderBackend.Application.Processors.Dtos;
using PcBuilderBackend.Domain.Entities;
using PcBuilderBackend.Domain.Interfaces;

namespace PcBuilderBackend.Application.Services
{
    public class ProcessorService : IProcessorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProcessorService> _logger;

        public ProcessorService(IUnitOfWork unitOfWork, ILogger<ProcessorService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IResult<PagedData<Processor>>> List(ODataQueryOptions<Processor> options, int page, int pageSize, CancellationToken ct = default)
        {
            try
            {
                var repo = _unitOfWork.GetRepository<Processor>();
                var query = repo.AsQueryable().AsNoTracking();

                if (options.Filter != null)
                    query = (IQueryable<Processor>)options.Filter.ApplyTo(query, new ODataQuerySettings());

                if (options.OrderBy != null)
                    query = options.OrderBy.ApplyTo(query, new ODataQuerySettings());

                var (items, totalCount) = await repo.GetPagedAsync(query, (page - 1) * pageSize, pageSize, ct);
                return Result<PagedData<Processor>>.Ok(PagedData<Processor>.Create(items, totalCount, page, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(List));
                return Result<PagedData<Processor>>.Error("Bir hata oluştu.");
            }
        }

        public async Task<IResult<Processor>> Get(int id, CancellationToken ct = default)
        {
            var entity = await _unitOfWork.GetRepository<Processor>().GetByIdAsync(id, ct);
            if (entity is null)
                return Result<Processor>.NotFound($"İşlemci {id} bulunamadı.");

            return Result<Processor>.Ok(entity);
        }

        public async Task<IResult<int>> Create(CreateProcessorRequest request, CancellationToken ct = default)
        {
            try
            {
                var repo = _unitOfWork.GetRepository<Processor>();
                var entity = request.Adapt<Processor>();
                await repo.AddAsync(entity, ct);
                await _unitOfWork.SaveChangesAsync(ct);
                return Result<int>.Created(entity.Id, "İşlemci başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(Create));
                return Result<int>.Error("Bir hata oluştu.");
            }
        }

        public async Task<IResult> Update(int id, UpdateProcessorRequest request, CancellationToken ct = default)
        {
            try
            {
                var repo = _unitOfWork.GetRepository<Processor>();
                var entity = await repo.GetByIdAsync(id, ct);
                if (entity is null)
                    return Result.NotFound($"İşlemci {id} bulunamadı.");

                request.Adapt(entity);
                repo.Update(entity);
                await _unitOfWork.SaveChangesAsync(ct);
                return Result.Ok("İşlemci başarıyla güncellendi.");
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
                var repo = _unitOfWork.GetRepository<Processor>();
                var entity = await repo.GetByIdAsync(id, ct);
                if (entity is null)
                    return Result.NotFound($"İşlemci {id} bulunamadı.");

                repo.Delete(entity);
                await _unitOfWork.SaveChangesAsync(ct);
                return Result.Ok("İşlemci başarıyla silindi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in {Method}", nameof(Delete));
                return Result.Error("Bir hata oluştu.");
            }
        }
    }
}
