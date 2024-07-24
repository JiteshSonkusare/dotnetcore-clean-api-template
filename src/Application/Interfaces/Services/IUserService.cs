using Shared.Wrapper;
using Shared.ApiClientHanlder;
using Application.Features.Users.Dtos;

namespace Application.Interfaces.Services;

public interface IUserService
{
	Task<Response<Result<List<UserDto>>>> GetUsersFromApiCall(CancellationToken cancellation);
}