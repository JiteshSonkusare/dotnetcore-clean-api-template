using Shared.Wrapper;
using Shared.ApiClientHanlder;
using Application.Features.Users.Dtos;

namespace Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<bool> IsUserIdUniqueAsync(string? userId);

    Task<Response<Result<List<UserDto>>>> GetUsersFromApiCall(CancellationToken cancellation);
}