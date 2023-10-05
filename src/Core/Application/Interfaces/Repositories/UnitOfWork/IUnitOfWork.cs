using Domain.Contracts;

namespace Application.Interfaces.Repositories;

public interface IUnitOfWork<TId> : IDisposable
{
    IRepositoryAsync<T, TId?> Repository<T>() where T : AuditableEntity<TId>;

    Task<int> CommitAsync(CancellationToken cancellationToken);

    Task<int> CommitAndRemoveCacheAsync(CancellationToken cancellationToken, params string[] cacheKeys);

    Task RollbackAsync();
}