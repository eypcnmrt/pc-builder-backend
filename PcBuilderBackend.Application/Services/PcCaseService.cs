using Mapster;
using Microsoft.AspNetCore.OData.Query;
using PcBuilderBackend.Application.Common;
using PcBuilderBackend.Application.Interfaces;
using PcBuilderBackend.Application.PcCases.Dtos;
using PcBuilderBackend.Domain.Entities;
using PcBuilderBackend.Domain.Interfaces;

namespace PcBuilderBackend.Application.Services
{
    public class PcCaseService : IPcCaseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PcCaseService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<IResult<PagedData<PcCase>>> Listele(ODataQueryOptions<PcCase> options, int page, int pageSize, CancellationToken ct = default)
        {
            try
            {
                var repo = _unitOfWork.GetRepository<PcCase>();
                var query = repo.AsQueryable();

                if (options.Filter != null)
                    query = (IQueryable<PcCase>)options.Filter.ApplyTo(query, new ODataQuerySettings());
                if (options.OrderBy != null)
                    query = options.OrderBy.ApplyTo(query, new ODataQuerySettings());

                var (items, totalCount) = await repo.GetPagedAsync(query, (page - 1) * pageSize, pageSize, ct);
                return Result<PagedData<PcCase>>.Ok(PagedData<PcCase>.Create(items, totalCount, page, pageSize));
            }
            catch (Exception ex) { return Result<PagedData<PcCase>>.Error(ex.Message); }
        }

        public async Task<IResult<PcCase>> Getir(int id, CancellationToken ct = default)
        {
            var entity = await _unitOfWork.GetRepository<PcCase>().GetByIdAsync(id, ct);
            return entity is null
                ? Result<PcCase>.NotFound($"Kasa {id} bulunamadı.")
                : Result<PcCase>.Ok(entity);
        }

        public async Task<IResult<int>> Ekle(CreatePcCaseRequest request, CancellationToken ct = default)
        {
            try
            {
                var repo = _unitOfWork.GetRepository<PcCase>();
                var entity = request.Adapt<PcCase>();
                await repo.AddAsync(entity, ct);
                await _unitOfWork.SaveChangesAsync(ct);
                return Result<int>.Created(entity.Id, "Kasa başarıyla eklendi.");
            }
            catch (Exception ex) { return Result<int>.Error(ex.Message); }
        }

        public async Task<IResult> Guncelle(int id, UpdatePcCaseRequest request, CancellationToken ct = default)
        {
            try
            {
                var repo = _unitOfWork.GetRepository<PcCase>();
                var entity = await repo.GetByIdAsync(id, ct);
                if (entity is null) return Result.NotFound($"Kasa {id} bulunamadı.");
                request.Adapt(entity);
                repo.Update(entity);
                await _unitOfWork.SaveChangesAsync(ct);
                return Result.Ok("Kasa başarıyla güncellendi.");
            }
            catch (Exception ex) { return Result.Error(ex.Message); }
        }

        public async Task<IResult> Sil(int id, CancellationToken ct = default)
        {
            try
            {
                var repo = _unitOfWork.GetRepository<PcCase>();
                var entity = await repo.GetByIdAsync(id, ct);
                if (entity is null) return Result.NotFound($"Kasa {id} bulunamadı.");
                repo.Delete(entity);
                await _unitOfWork.SaveChangesAsync(ct);
                return Result.Ok("Kasa başarıyla silindi.");
            }
            catch (Exception ex) { return Result.Error(ex.Message); }
        }
    }
}
