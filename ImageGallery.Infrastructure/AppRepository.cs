using System.Linq.Expressions;
using ImageGallery.Infrastructure.Persistence;
using ImageGallery.Shared.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ImageGallery.Infrastructure;

public class AppRepository<TEntity>(AppDbContext dbContext) : IAppRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly DbSet<TEntity> _dbSet = dbContext.Set<TEntity>();
    private readonly AppDbContext _dbContext = dbContext;

    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public Task RemoveAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
    }

    public IQueryable<TEntity> GetAsQuery(bool isReadonly = false)
    {
        return isReadonly ? _dbSet.AsNoTracking() : _dbSet;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync() > 0;
    }
    public Task<IQueryable<TEntity>> GetWithIncludesAsync(bool isReadonly = false, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = isReadonly ? _dbSet.AsNoTracking().AsQueryable() : _dbSet.AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return Task.FromResult(query);
    }

}
