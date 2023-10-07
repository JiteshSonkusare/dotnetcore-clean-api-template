using Shared.ApiClientHanlder;

namespace Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<bool> UserIdExists(string? userId);

    Task<Response<string>> GetUsersFromApiCall(CancellationToken cancellation);
}