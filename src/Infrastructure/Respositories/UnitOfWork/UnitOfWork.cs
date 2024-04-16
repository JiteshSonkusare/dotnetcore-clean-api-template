using LazyCache;
using Domain.Contracts;
using System.Collections;
using Infrastructure.Context;
using Application.Interfaces.Repositories;

namespace Infrastructure.Respositories;

public class UnitOfWork<TId> : IUnitOfWork<TId>
{
    private bool disposed;
    private Hashtable? _repositories;
    private readonly IAppCache _cache;
    private readonly ApplicationDBContext _dbContext;

    public UnitOfWork(IAppCache cache, ApplicationDBContext dbContext)
    {
        _cache = cache;
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public IRepositoryAsync<TEntity, TId?> Repository<TEntity>() where TEntity : AuditableEntity<TId>
    {
        _repositories ??= new Hashtable();

        var type = typeof(TEntity).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(RepositoryAsync<,>);

            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity), typeof(TId)), _dbContext);

            _repositories.Add(type, repositoryInstance);
        }

        return (IRepositoryAsync<TEntity, TId?>)_repositories[type]!;
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> CommitAndRemoveCacheAsync(CancellationToken cancellationToken, params string[] cacheKeys)
    {
        var result = await _dbContext.SaveChangesAsync(cancellationToken);
        foreach (var cacheKey in cacheKeys)
        {
            _cache.Remove(cacheKey);
        }
        return result;
    }

    public Task RollbackAsync()
    {
        _dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                //dispose managed resources
                _dbContext.Dispose();
            }
        }
        //dispose unmanaged resources
        disposed = true;
    }
}