using Mapster;
using Microsoft.AspNetCore.OData.Query;
using PcBuilderBackend.Application.Common;
using PcBuilderBackend.Application.Interfaces;
using PcBuilderBackend.Application.Motherboards.Dtos;
using PcBuilderBackend.Domain.Entities;
using PcBuilderBackend.Domain.Interfaces;

namespace PcBuilderBackend.Application.Services
{
    public class MotherboardService : IMotherboardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MotherboardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult<PagedData<Motherboard>>> Listele(ODataQueryOptions<Motherboard> options, int page, int pageSize, CancellationToken ct = default)
        {
            try
            {
                var repo = _unitOfWork.GetRepository<Motherboard>();
                var query = repo.AsQueryable();

                if (options.Filter != null)
                    query = (IQueryable<Motherboard>)options.Filter.ApplyTo(query, new ODataQuerySettings());

                if (options.OrderBy != null)
                    query = options.OrderBy.ApplyTo(query, new ODataQuerySettings());

                var (items, totalCount) = await repo.GetPagedAsync(query, (page - 1) * pageSize, pageSize, ct);
                var pageCount = (int)Math.Ceiling((double)totalCount / pageSize);

                return Result<PagedData<Motherboard>>.Ok(new PagedData<Motherboard>
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
                return Result<PagedData<Motherboard>>.Error(ex.Message);
            }
        }

        public async Task<IResult<PagedData<Motherboard>>> UyumluListele(string socket, int page, int pageSize, CancellationToken ct = default)
        {
            try
            {
                var repo = _unitOfWork.GetRepository<Motherboard>();
                var query = repo.AsQueryable().Where(m => m.Socket == socket);

                var (items, totalCount) = await repo.GetPagedAsync(query, (page - 1) * pageSize, pageSize, ct);
                var pageCount = (int)Math.Ceiling((double)totalCount / pageSize);

                return Result<PagedData<Motherboard>>.Ok(new PagedData<Motherboard>
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
                return Result<PagedData<Motherboard>>.Error(ex.Message);
            }
        }

        public async Task<IResult<Motherboard>> Getir(int id, CancellationToken ct = default)
        {
            var entity = await _unitOfWork.GetRepository<Motherboard>().GetByIdAsync(id, ct);
            if (entity is null)
                return Result<Motherboard>.NotFound($"Anakart {id} bulunamadı.");

            return Result<Motherboard>.Ok(entity);
        }

        public async Task<IResult<int>> Ekle(CreateMotherboardRequest request, CancellationToken ct = default)
        {
            try
            {
                var repo = _unitOfWork.GetRepository<Motherboard>();
                var entity = request.Adapt<Motherboard>();
                await repo.AddAsync(entity, ct);
                await _unitOfWork.SaveChangesAsync(ct);
                return Result<int>.Created(entity.Id, "Anakart başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                return Result<int>.Error(ex.Message);
            }
        }

        public async Task<IResult> Guncelle(int id, UpdateMotherboardRequest request, CancellationToken ct = default)
        {
            try
            {
                var repo = _unitOfWork.GetRepository<Motherboard>();
                var entity = await repo.GetByIdAsync(id, ct);
                if (entity is null)
                    return Result.NotFound($"Anakart {id} bulunamadı.");

                request.Adapt(entity);
                repo.Update(entity);
                await _unitOfWork.SaveChangesAsync(ct);
                return Result.Ok("Anakart başarıyla güncellendi.");
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
                var repo = _unitOfWork.GetRepository<Motherboard>();
                var entity = await repo.GetByIdAsync(id, ct);
                if (entity is null)
                    return Result.NotFound($"Anakart {id} bulunamadı.");

                repo.Delete(entity);
                await _unitOfWork.SaveChangesAsync(ct);
                return Result.Ok("Anakart başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }
    }
}
