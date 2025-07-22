using System.Linq.Expressions;

namespace ImageGallery.Shared.Abstractions;

public interface IAppRepository<TEntity> where TEntity : BaseEntity
{
    Task AddAsync(TEntity entity);
    Task RemoveAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task<TEntity?> GetByIdAsync(Guid id);
    IQueryable<TEntity> GetAsQuery(bool isReadOnly = false);
    Task<IQueryable<TEntity>> GetWithIncludesAsync(bool isReadOnly = false, params Expression<Func<TEntity, object>>[] includes);
    Task<bool> SaveChangesAsync();
}