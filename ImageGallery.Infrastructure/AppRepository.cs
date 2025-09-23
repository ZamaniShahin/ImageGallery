using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
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

    public async Task<TEntity?> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate,
        bool isReadOnly = false,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] includes)
    {
        var query = BuildQuery(isReadOnly, includes);
        return await query.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<List<TProjection>> ListAsync<TProjection>(
        Func<IQueryable<TEntity>, IQueryable<TProjection>> queryBuilder,
        bool isReadOnly = false,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] includes)
    {
        var query = BuildQuery(isReadOnly, includes);
        return await queryBuilder(query).ToListAsync(cancellationToken);
    }

    public async Task<TProjection?> SingleOrDefaultAsync<TProjection>(
        Func<IQueryable<TEntity>, IQueryable<TProjection>> queryBuilder,
        bool isReadOnly = false,
        CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object>>[] includes)
    {
        var query = BuildQuery(isReadOnly, includes);
        return await queryBuilder(query).SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _dbContext.SaveChangesAsync() > 0;
    }

    private IQueryable<TEntity> BuildQuery(
        bool isReadOnly,
        params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = isReadOnly ? _dbSet.AsNoTracking() : _dbSet;
        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return query;
    }
}
