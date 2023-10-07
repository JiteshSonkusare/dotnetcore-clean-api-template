using Application.Features.Users.Queries.ViewModels;
using Shared.Wrapper;
using Shared.ApiClientHanlder;

namespace Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<bool> UserIdExists(string? userId);

    Task<Response<Result<List<UserViewModel>>>> GetUsersFromApiCall(CancellationToken cancellation);
}