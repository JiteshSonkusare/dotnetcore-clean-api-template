namespace Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<bool> UserIdExists(string? userId);
}