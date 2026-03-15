using System.Linq.Expressions;

namespace PcBuilderBackend.Domain.Interfaces
{
    public interface IRepository<T> where T : class, IEntity
    {
        IQueryable<T> AsQueryable();
        Task<T?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default);
        Task<T?> FirstOrDefaultAsync(IQueryable<T> query, CancellationToken ct = default);
        Task<(List<T> Items, int TotalCount)> GetPagedAsync(IQueryable<T> query, int skip, int take, CancellationToken ct = default);
        Task AddAsync(T entity, CancellationToken ct = default);
        void Update(T entity);
        void Delete(T entity);
    }
}
