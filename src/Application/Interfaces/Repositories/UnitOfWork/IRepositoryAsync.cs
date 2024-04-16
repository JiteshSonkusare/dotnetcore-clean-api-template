using Domain.Contracts;

namespace Application.Interfaces.Repositories;

public interface IRepositoryAsync<T, in TId> where T : class, IEntity<TId>
{
    IQueryable<T> Entities { get; }

    Task<T> GetByIdAsync(TId id);

    Task<List<T>> GetAllAsync();

    Task<T> AddAsync(T entity);

    Task UpdateAsync(T entity);

    Task DeleteAsync(T entity);
}