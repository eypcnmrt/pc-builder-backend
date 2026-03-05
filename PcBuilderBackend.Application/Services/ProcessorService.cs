using Mapster;
using Microsoft.AspNetCore.OData.Query;
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

        public ProcessorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult<PagedData<Processor>>> Listele(ODataQueryOptions<Processor> options, int page, int pageSize, CancellationToken ct = default)
        {
            try
            {
                var repo = _unitOfWork.GetRepository<Processor>();
                var query = repo.AsQueryable();

                if (options.Filter != null)
                    query = (IQueryable<Processor>)options.Filter.ApplyTo(query, new ODataQuerySettings());

                if (options.OrderBy != null)
                    query = options.OrderBy.ApplyTo(query, new ODataQuerySettings());

                var (items, totalCount) = await repo.GetPagedAsync(query, (page - 1) * pageSize, pageSize, ct);
                var pageCount = (int)Math.Ceiling((double)totalCount / pageSize);

                return Result<PagedData<Processor>>.Ok(new PagedData<Processor>
                {
                    Items = items,
                    TotalCount = totalCount,
                    PageCount = pageCount,
                    Page = page,
                    PageSize = pageSize
                });
            }
            catch (Exception ex)
            {
                return Result<PagedData<Processor>>.Error(ex.Message);
            }
        }

        public async Task<IResult<Processor>> Getir(int id, CancellationToken ct = default)
        {
            var entity = await _unitOfWork.GetRepository<Processor>().GetByIdAsync(id, ct);
            if (entity is null)
                return Result<Processor>.NotFound($"İşlemci {id} bulunamadı.");

            return Result<Processor>.Ok(entity);
        }

        public async Task<IResult<int>> Ekle(CreateProcessorRequest request, CancellationToken ct = default)
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
                return Result<int>.Error(ex.Message);
            }
        }

        public async Task<IResult> Guncelle(int id, UpdateProcessorRequest request, CancellationToken ct = default)
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
                return Result.Error(ex.Message);
            }
        }

        public async Task<IResult> Sil(int id, CancellationToken ct = default)
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
                return Result.Error(ex.Message);
            }
        }
    }
}
