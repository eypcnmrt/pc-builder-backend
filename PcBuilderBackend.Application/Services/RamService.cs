using Mapster;
using Microsoft.AspNetCore.OData.Query;
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

        public RamService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult<PagedData<Ram>>> Listele(ODataQueryOptions<Ram> options, int page, int pageSize, CancellationToken ct = default)
        {
            try
            {
                var repo = _unitOfWork.GetRepository<Ram>();
                var query = repo.AsQueryable();

                if (options.Filter != null)
                    query = (IQueryable<Ram>)options.Filter.ApplyTo(query, new ODataQuerySettings());

                if (options.OrderBy != null)
                    query = options.OrderBy.ApplyTo(query, new ODataQuerySettings());

                var (items, totalCount) = await repo.GetPagedAsync(query, (page - 1) * pageSize, pageSize, ct);
                var pageCount = (int)Math.Ceiling((double)totalCount / pageSize);

                return Result<PagedData<Ram>>.Ok(new PagedData<Ram>
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
                return Result<PagedData<Ram>>.Error(ex.Message);
            }
        }

        public async Task<IResult<Ram>> Getir(int id, CancellationToken ct = default)
        {
            var entity = await _unitOfWork.GetRepository<Ram>().GetByIdAsync(id, ct);
            if (entity is null)
                return Result<Ram>.NotFound($"RAM {id} bulunamadı.");

            return Result<Ram>.Ok(entity);
        }

        public async Task<IResult<int>> Ekle(CreateRamRequest request, CancellationToken ct = default)
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
                return Result<int>.Error(ex.Message);
            }
        }

        public async Task<IResult> Guncelle(int id, UpdateRamRequest request, CancellationToken ct = default)
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
                return Result.Error(ex.Message);
            }
        }

        public async Task<IResult> Sil(int id, CancellationToken ct = default)
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
                return Result.Error(ex.Message);
            }
        }
    }
}
