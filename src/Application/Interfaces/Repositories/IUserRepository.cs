using Shared.Wrapper;
using Shared.ApiClientHanlder;
using Application.Features.Users.Queries.ViewModels;

namespace Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<bool> UserIdExists(string? userId);

    Task<Response<Result<List<UserViewModel>>>> GetUsersFromApiCall(CancellationToken cancellation);
}