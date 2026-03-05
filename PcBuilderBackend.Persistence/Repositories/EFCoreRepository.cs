using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PcBuilderBackend.Domain.Interfaces;
using PcBuilderBackend.Persistence.Contexts;

namespace PcBuilderBackend.Persistence.Repositories
{
    public class EFCoreRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly DbSet<T> _dbSet;

        public EFCoreRepository(AppDbContext context)
        {
            _dbSet = context.Set<T>();
        }

        public IQueryable<T> AsQueryable() =>
            _dbSet.AsQueryable();

        public Task<T?> GetByIdAsync(int id, CancellationToken ct = default) =>
            _dbSet.FirstOrDefaultAsync(e => e.Id == id, ct);

        public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default) =>
            _dbSet.AnyAsync(predicate, ct);

        public Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default) =>
            _dbSet.FirstOrDefaultAsync(predicate, ct);

        public async Task<(List<T> Items, int TotalCount)> GetPagedAsync(IQueryable<T> query, int skip, int take, CancellationToken ct = default)
        {
            var totalCount = await query.CountAsync(ct);
            var items = await query.Skip(skip).Take(take).ToListAsync(ct);
            return (items, totalCount);
        }

        public Task AddAsync(T entity, CancellationToken ct = default) =>
            _dbSet.AddAsync(entity, ct).AsTask();

        public void Update(T entity) =>
            _dbSet.Update(entity);

        public void Delete(T entity) =>
            _dbSet.Remove(entity);
    }
}
