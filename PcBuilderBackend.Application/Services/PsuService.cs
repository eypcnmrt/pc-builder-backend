using Mapster;
using Microsoft.AspNetCore.OData.Query;
using PcBuilderBackend.Application.Common;
using PcBuilderBackend.Application.Interfaces;
using PcBuilderBackend.Application.Psus.Dtos;
using PcBuilderBackend.Domain.Entities;
using PcBuilderBackend.Domain.Interfaces;

namespace PcBuilderBackend.Application.Services
{
    public class PsuService : IPsuService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PsuService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<IResult<PagedData<Psu>>> Listele(ODataQueryOptions<Psu> options, int page, int pageSize, CancellationToken ct = default)
        {
            try
            {
                var repo = _unitOfWork.GetRepository<Psu>();
                var query = repo.AsQueryable();

                if (options.Filter != null)
                    query = (IQueryable<Psu>)options.Filter.ApplyTo(query, new ODataQuerySettings());
                if (options.OrderBy != null)
                    query = options.OrderBy.ApplyTo(query, new ODataQuerySettings());

                var (items, totalCount) = await repo.GetPagedAsync(query, (page - 1) * pageSize, pageSize, ct);
                return Result<PagedData<Psu>>.Ok(PagedData<Psu>.Create(items, totalCount, page, pageSize));
            }
            catch (Exception ex) { return Result<PagedData<Psu>>.Error(ex.Message); }
        }

        public async Task<IResult<Psu>> Getir(int id, CancellationToken ct = default)
        {
            var entity = await _unitOfWork.GetRepository<Psu>().GetByIdAsync(id, ct);
            return entity is null
                ? Result<Psu>.NotFound($"Güç kaynağı {id} bulunamadı.")
                : Result<Psu>.Ok(entity);
        }

        public async Task<IResult<int>> Ekle(CreatePsuRequest request, CancellationToken ct = default)
        {
            try
            {
                var repo = _unitOfWork.GetRepository<Psu>();
                var entity = request.Adapt<Psu>();
                await repo.AddAsync(entity, ct);
                await _unitOfWork.SaveChangesAsync(ct);
                return Result<int>.Created(entity.Id, "Güç kaynağı başarıyla eklendi.");
            }
            catch (Exception ex) { return Result<int>.Error(ex.Message); }
        }

        public async Task<IResult> Guncelle(int id, UpdatePsuRequest request, CancellationToken ct = default)
        {
            try
            {
                var repo = _unitOfWork.GetRepository<Psu>();
                var entity = await repo.GetByIdAsync(id, ct);
                if (entity is null) return Result.NotFound($"Güç kaynağı {id} bulunamadı.");
                request.Adapt(entity);
                repo.Update(entity);
                await _unitOfWork.SaveChangesAsync(ct);
                return Result.Ok("Güç kaynağı başarıyla güncellendi.");
            }
            catch (Exception ex) { return Result.Error(ex.Message); }
        }

        public async Task<IResult> Sil(int id, CancellationToken ct = default)
        {
            try
            {
                var repo = _unitOfWork.GetRepository<Psu>();
                var entity = await repo.GetByIdAsync(id, ct);
                if (entity is null) return Result.NotFound($"Güç kaynağı {id} bulunamadı.");
                repo.Delete(entity);
                await _unitOfWork.SaveChangesAsync(ct);
                return Result.Ok("Güç kaynağı başarıyla silindi.");
            }
            catch (Exception ex) { return Result.Error(ex.Message); }
        }
    }
}
