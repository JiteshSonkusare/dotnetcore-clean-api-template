using Shared.Wrapper;
using Application.Features.Users.Dtos;

namespace Application.Interfaces.Services;

public interface IUserService
{
    Task<Result<List<UserDto>>> GetUsersFromApiCall(CancellationToken cancellation);
}