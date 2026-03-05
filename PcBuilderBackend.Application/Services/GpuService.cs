using Mapster;
using Microsoft.AspNetCore.OData.Query;
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

        public GpuService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult<PagedData<Gpu>>> Listele(ODataQueryOptions<Gpu> options, int page, int pageSize, CancellationToken ct = default)
        {
            try
            {
                var repo = _unitOfWork.GetRepository<Gpu>();
                var query = repo.AsQueryable();

                if (options.Filter != null)
                    query = (IQueryable<Gpu>)options.Filter.ApplyTo(query, new ODataQuerySettings());

                if (options.OrderBy != null)
                    query = options.OrderBy.ApplyTo(query, new ODataQuerySettings());

                var (items, totalCount) = await repo.GetPagedAsync(query, (page - 1) * pageSize, pageSize, ct);
                var pageCount = (int)Math.Ceiling((double)totalCount / pageSize);

                return Result<PagedData<Gpu>>.Ok(new PagedData<Gpu>
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
                return Result<PagedData<Gpu>>.Error(ex.Message);
            }
        }

        public async Task<IResult<Gpu>> Getir(int id, CancellationToken ct = default)
        {
            var entity = await _unitOfWork.GetRepository<Gpu>().GetByIdAsync(id, ct);
            if (entity is null)
                return Result<Gpu>.NotFound($"Ekran kartı {id} bulunamadı.");

            return Result<Gpu>.Ok(entity);
        }

        public async Task<IResult<int>> Ekle(CreateGpuRequest request, CancellationToken ct = default)
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
                return Result<int>.Error(ex.Message);
            }
        }

        public async Task<IResult> Guncelle(int id, UpdateGpuRequest request, CancellationToken ct = default)
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
                return Result.Error(ex.Message);
            }
        }

        public async Task<IResult> Sil(int id, CancellationToken ct = default)
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
                return Result.Error(ex.Message);
            }
        }
    }
}
