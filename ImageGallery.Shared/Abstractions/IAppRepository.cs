using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;

namespace ImageGallery.Shared.Abstractions;

public interface IAppRepository<TEntity> where TEntity : BaseEntity
{
    Task AddAsync(TEntity entity);
    Task RemoveAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<TEntity?> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate,
        bool isReadOnly = false,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] includes);
    Task<List<TProjection>> ListAsync<TProjection>(
        Func<IQueryable<TEntity>, IQueryable<TProjection>> queryBuilder,
        bool isReadOnly = false,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] includes);
    Task<TProjection?> SingleOrDefaultAsync<TProjection>(
        Func<IQueryable<TEntity>, IQueryable<TProjection>> queryBuilder,
        bool isReadOnly = false,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] includes);
    Task<bool> SaveChangesAsync();
}

