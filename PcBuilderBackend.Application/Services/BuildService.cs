using Mapster;
using PcBuilderBackend.Application.Builds.Dtos;
using PcBuilderBackend.Application.Common;
using PcBuilderBackend.Application.Interfaces;
using PcBuilderBackend.Domain.Entities;
using PcBuilderBackend.Domain.Interfaces;

namespace PcBuilderBackend.Application.Services
{
    public class BuildService : IBuildService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BuildService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult<Build>> GetCurrent(int userId, CancellationToken ct = default)
        {
            try
            {
                var repo = _unitOfWork.GetRepository<Build>();
                var query = repo.AsQueryable()
                    .Where(b => b.UserId == userId)
                    .OrderByDescending(b => b.UpdatedAt);
                var build = await repo.FirstOrDefaultAsync(query, ct);

                if (build is null)
                {
                    build = new Build
                    {
                        UserId = userId,
                        Name = "Yeni Yapılandırma",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };
                    await repo.AddAsync(build, ct);
                    await _unitOfWork.SaveChangesAsync(ct);
                }

                await PopulateComponents(build, ct);
                return Result<Build>.Ok(build);
            }
            catch (Exception ex)
            {
                return Result<Build>.Error(ex.Message);
            }
        }

        public async Task<IResult<Build>> GetById(int id, int userId, CancellationToken ct = default)
        {
            try
            {
                var build = await _unitOfWork.GetRepository<Build>()
                    .FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId, ct);

                if (build is null)
                    return Result<Build>.NotFound($"Build {id} bulunamadı.");

                await PopulateComponents(build, ct);
                return Result<Build>.Ok(build);
            }
            catch (Exception ex)
            {
                return Result<Build>.Error(ex.Message);
            }
        }

        public async Task<IResult<int>> Create(CreateBuildRequest request, int userId, CancellationToken ct = default)
        {
            try
            {
                var build = new Build
                {
                    UserId = userId,
                    Name = request.Name,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _unitOfWork.GetRepository<Build>().AddAsync(build, ct);
                await _unitOfWork.SaveChangesAsync(ct);

                return Result<int>.Created(build.Id, "Build başarıyla oluşturuldu.");
            }
            catch (Exception ex)
            {
                return Result<int>.Error(ex.Message);
            }
        }

        public async Task<IResult<Build>> Update(int id, UpdateBuildRequest request, int userId, CancellationToken ct = default)
        {
            try
            {
                var repo = _unitOfWork.GetRepository<Build>();
                var build = await repo.FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId, ct);

                if (build is null)
                    return Result<Build>.NotFound($"Build {id} bulunamadı.");

                var activityRepo = _unitOfWork.GetRepository<BuildActivity>();

                await TrackChange(activityRepo, build, "Processor", build.ProcessorId, request.ProcessorId, ct);
                await TrackChange(activityRepo, build, "Motherboard", build.MotherboardId, request.MotherboardId, ct);
                await TrackChange(activityRepo, build, "Gpu", build.GpuId, request.GpuId, ct);
                await TrackChange(activityRepo, build, "Ram", build.RamId, request.RamId, ct);
                await TrackChange(activityRepo, build, "Storage", build.StorageId, request.StorageId, ct);
                await TrackChange(activityRepo, build, "Psu", build.PsuId, request.PsuId, ct);
                await TrackChange(activityRepo, build, "PcCase", build.PcCaseId, request.PcCaseId, ct);
                await TrackChange(activityRepo, build, "Cooler", build.CoolerId, request.CoolerId, ct);

                if (request.Name is not null) build.Name = request.Name;
                if (request.ProcessorId.HasValue) build.ProcessorId = request.ProcessorId == 0 ? null : request.ProcessorId;
                if (request.MotherboardId.HasValue) build.MotherboardId = request.MotherboardId == 0 ? null : request.MotherboardId;
                if (request.GpuId.HasValue) build.GpuId = request.GpuId == 0 ? null : request.GpuId;
                if (request.RamId.HasValue) build.RamId = request.RamId == 0 ? null : request.RamId;
                if (request.StorageId.HasValue) build.StorageId = request.StorageId == 0 ? null : request.StorageId;
                if (request.PsuId.HasValue) build.PsuId = request.PsuId == 0 ? null : request.PsuId;
                if (request.PcCaseId.HasValue) build.PcCaseId = request.PcCaseId == 0 ? null : request.PcCaseId;
                if (request.CoolerId.HasValue) build.CoolerId = request.CoolerId == 0 ? null : request.CoolerId;

                build.UpdatedAt = DateTime.UtcNow;
                repo.Update(build);
                await _unitOfWork.SaveChangesAsync(ct);

                await PopulateComponents(build, ct);
                return Result<Build>.Ok(build);
            }
            catch (Exception ex)
            {
                return Result<Build>.Error(ex.Message);
            }
        }

        public async Task<IResult> Delete(int id, int userId, CancellationToken ct = default)
        {
            try
            {
                var repo = _unitOfWork.GetRepository<Build>();
                var build = await repo.FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId, ct);

                if (build is null)
                    return Result.NotFound($"Build {id} bulunamadı.");

                repo.Delete(build);
                await _unitOfWork.SaveChangesAsync(ct);
                return Result.Ok("Build başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return Result.Error(ex.Message);
            }
        }

        public async Task<IResult<PagedData<BuildActivityResponse>>> GetActivities(int buildId, int userId, int page, int pageSize, CancellationToken ct = default)
        {
            try
            {
                var buildExists = await _unitOfWork.GetRepository<Build>()
                    .AnyAsync(b => b.Id == buildId && b.UserId == userId, ct);

                if (!buildExists)
                    return Result<PagedData<BuildActivityResponse>>.NotFound($"Build {buildId} bulunamadı.");

                var repo = _unitOfWork.GetRepository<BuildActivity>();
                var query = repo.AsQueryable()
                    .Where(a => a.BuildId == buildId)
                    .OrderByDescending(a => a.CreatedAt);

                var (items, totalCount) = await repo.GetPagedAsync(query, (page - 1) * pageSize, pageSize, ct);

                return Result<PagedData<BuildActivityResponse>>.Ok(new PagedData<BuildActivityResponse>
                {
                    Items = items.Adapt<List<BuildActivityResponse>>(),
                    TotalCount = totalCount,
                    PageCount = (int)Math.Ceiling((double)totalCount / pageSize),
                    Page = page,
                    PageSize = pageSize
                });
            }
            catch (Exception ex)
            {
                return Result<PagedData<BuildActivityResponse>>.Error(ex.Message);
            }
        }

        private async Task PopulateComponents(Build build, CancellationToken ct)
        {
            if (build.ProcessorId.HasValue)
                build.Processor = await _unitOfWork.GetRepository<Processor>().GetByIdAsync(build.ProcessorId.Value, ct);
            if (build.MotherboardId.HasValue)
                build.Motherboard = await _unitOfWork.GetRepository<Motherboard>().GetByIdAsync(build.MotherboardId.Value, ct);
            if (build.GpuId.HasValue)
                build.Gpu = await _unitOfWork.GetRepository<Gpu>().GetByIdAsync(build.GpuId.Value, ct);
            if (build.RamId.HasValue)
                build.Ram = await _unitOfWork.GetRepository<Ram>().GetByIdAsync(build.RamId.Value, ct);
            if (build.StorageId.HasValue)
                build.Storage = await _unitOfWork.GetRepository<Storage>().GetByIdAsync(build.StorageId.Value, ct);
            if (build.PsuId.HasValue)
                build.Psu = await _unitOfWork.GetRepository<Psu>().GetByIdAsync(build.PsuId.Value, ct);
            if (build.PcCaseId.HasValue)
                build.PcCase = await _unitOfWork.GetRepository<PcCase>().GetByIdAsync(build.PcCaseId.Value, ct);
            if (build.CoolerId.HasValue)
                build.Cooler = await _unitOfWork.GetRepository<Cooler>().GetByIdAsync(build.CoolerId.Value, ct);
        }

        private async Task TrackChange(IRepository<BuildActivity> repo, Build build, string componentType, int? oldId, int? newId, CancellationToken ct)
        {
            if (!newId.HasValue) return;

            var actualNewId = newId == 0 ? null : newId;
            if (oldId == actualNewId) return;

            string action;
            string description;

            if (oldId is null && actualNewId is not null)
            {
                action = "Added";
                description = $"{componentType} eklendi (ID: {actualNewId})";
            }
            else if (oldId is not null && actualNewId is null)
            {
                action = "Removed";
                description = $"{componentType} kaldırıldı (ID: {oldId})";
            }
            else
            {
                action = "Updated";
                description = $"{componentType} güncellendi (ID: {oldId} → {actualNewId})";
            }

            await repo.AddAsync(new BuildActivity
            {
                BuildId = build.Id,
                ComponentType = componentType,
                ComponentId = actualNewId ?? oldId,
                Action = action,
                Description = description,
                CreatedAt = DateTime.UtcNow
            }, ct);
        }
    }
}
