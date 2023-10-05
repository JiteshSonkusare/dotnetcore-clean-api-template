using Domain.Contracts;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces.Repositories;

namespace Infrastructure.Respositories;

public class RepositoryAsync<T, TId> : IRepositoryAsync<T, TId?> where T : AuditableEntity<TId>
{
    private readonly ApplicationDBContext _dbContext;

    public RepositoryAsync(ApplicationDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<T> Entities => _dbContext.Set<T>();

    public async Task<T> AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        return entity;
    }

    public Task DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<T> GetByIdAsync(TId? id)
    {
        return await _dbContext.Set<T>().FindAsync(id) ?? default!;
    }

    public async Task UpdateAsync(T entity)
    {
        T? exist = await _dbContext.Set<T>().FindAsync(entity.Id);
        if (exist != null)
        {
            _dbContext.Entry(exist).CurrentValues.SetValues(entity);
            await Task.CompletedTask;
        }
    }
}
