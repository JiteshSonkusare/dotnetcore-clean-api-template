using MediatR;
using AutoMapper;
using Shared.Wrapper;
using Application.Features.Users.Dtos;
using Application.Interfaces.Services;
using Application.Common.ExceptionHandlers;

namespace Application.Features.Users.Queries;

internal class GetUserByAPIQueryHandler(
	IMapper mapper, 
	IUserService userService) 
	: IRequestHandler<GetUserByAPIQuery, Result<List<UserDto>>>
{
	private readonly IMapper _mapper = mapper;
	private readonly IUserService _userService = userService;

	public async Task<Result<List<UserDto>>> Handle(GetUserByAPIQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var response = await _userService.GetUsersFromApiCall(cancellationToken);
            if (response.IsFailure)
				return Result.Failure<List<UserDto>>(UserError.NotFound);
			var mappedUser = _mapper.Map<List<UserDto>>(response.Data);
			return Result.Success(mappedUser);
		}
		catch (Exception ex)
		{
			throw ex.With("Failed to get user data from api!", ex.Message);
		}
	}
}