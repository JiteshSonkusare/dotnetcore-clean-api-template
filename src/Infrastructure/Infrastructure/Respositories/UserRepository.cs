using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces.Repositories;

namespace Infrastructure.Respositories;

public class UserRepository : IUserRepository
{
    private readonly IRepositoryAsync<User, Guid> _repository;

    public UserRepository(IRepositoryAsync<User, Guid> repository)
    {
        _repository = repository;
    }

    public async Task<bool> UserIdExists(string? userId)
    {
        return await _repository.Entities.AnyAsync(b => b.UserId == userId);
    }
}