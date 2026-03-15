using Mapster;
using Microsoft.AspNetCore.OData.Query;
using PcBuilderBackend.Application.Common;
using PcBuilderBackend.Application.Interfaces;
using PcBuilderBackend.Application.Storages.Dtos;
using PcBuilderBackend.Domain.Entities;
using PcBuilderBackend.Domain.Interfaces;

namespace PcBuilderBackend.Application.Services
{
    public class StorageService : IStorageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StorageService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<IResult<PagedData<Storage>>> Listele(ODataQueryOptions<Storage> options, int page, int pageSize, CancellationToken ct = default)
        {
            try
            {
                var repo = _unitOfWork.GetRepository<Storage>();
                var query = repo.AsQueryable();

                if (options.Filter != null)
                    query = (IQueryable<Storage>)options.Filter.ApplyTo(query, new ODataQuerySettings());
                if (options.OrderBy != null)
                    query = options.OrderBy.ApplyTo(query, new ODataQuerySettings());

                var (items, totalCount) = await repo.GetPagedAsync(query, (page - 1) * pageSize, pageSize, ct);
                return Result<PagedData<Storage>>.Ok(PagedData<Storage>.Create(items, totalCount, page, pageSize));
            }
            catch (Exception ex) { return Result<PagedData<Storage>>.Error(ex.Message); }
        }

        public async Task<IResult<Storage>> Getir(int id, CancellationToken ct = default)
        {
            var entity = await _unitOfWork.GetRepository<Storage>().GetByIdAsync(id, ct);
            return entity is null
                ? Result<Storage>.NotFound($"Depolama birimi {id} bulunamadı.")
                : Result<Storage>.Ok(entity);
        }

        public async Task<IResult<int>> Ekle(CreateStorageRequest request, CancellationToken ct = default)
        {
            try
            {
                var repo = _unitOfWork.GetRepository<Storage>();
                var entity = request.Adapt<Storage>();
                await repo.AddAsync(entity, ct);
                await _unitOfWork.SaveChangesAsync(ct);
                return Result<int>.Created(entity.Id, "Depolama birimi başarıyla eklendi.");
            }
            catch (Exception ex) { return Result<int>.Error(ex.Message); }
        }

        public async Task<IResult> Guncelle(int id, UpdateStorageRequest request, CancellationToken ct = default)
        {
            try
            {
                var repo = _unitOfWork.GetRepository<Storage>();
                var entity = await repo.GetByIdAsync(id, ct);
                if (entity is null) return Result.NotFound($"Depolama birimi {id} bulunamadı.");
                request.Adapt(entity);
                repo.Update(entity);
                await _unitOfWork.SaveChangesAsync(ct);
                return Result.Ok("Depolama birimi başarıyla güncellendi.");
            }
            catch (Exception ex) { return Result.Error(ex.Message); }
        }

        public async Task<IResult> Sil(int id, CancellationToken ct = default)
        {
            try
            {
                var repo = _unitOfWork.GetRepository<Storage>();
                var entity = await repo.GetByIdAsync(id, ct);
                if (entity is null) return Result.NotFound($"Depolama birimi {id} bulunamadı.");
                repo.Delete(entity);
                await _unitOfWork.SaveChangesAsync(ct);
                return Result.Ok("Depolama birimi başarıyla silindi.");
            }
            catch (Exception ex) { return Result.Error(ex.Message); }
        }
    }
}
