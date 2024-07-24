namespace Application.Interfaces.Repositories;

public interface IUserRepository
{
	Task<bool> IsUserIdUniqueAsync(string? userId);
}