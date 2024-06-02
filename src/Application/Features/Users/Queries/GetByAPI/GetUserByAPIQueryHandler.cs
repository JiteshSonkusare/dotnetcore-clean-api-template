using MediatR;
using AutoMapper;
using Shared.Wrapper;
using Application.Features.Users.Dtos;
using Application.Interfaces.Repositories;
using Application.Common.ExceptionHandlers;

namespace Application.Features.Users.Queries;

internal class GetUserByAPIQueryHandler : IRequestHandler<GetUserByAPIQuery, Result<List<UserDto>>>
{
	private readonly IUserRepository _userRepository;
	private readonly IMapper _mapper;

	public GetUserByAPIQueryHandler(IUserRepository userRepository, IMapper mapper)
	{
		_userRepository = userRepository;
		_mapper = mapper;
	}

	public async Task<Result<List<UserDto>>> Handle(GetUserByAPIQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var response = await _userRepository.GetUsersFromApiCall(cancellationToken);
			if (response.Data?.Data == null)
				return await Result<List<UserDto>>.FailureAsync(UserError.NotFound);
			var mappedUser = _mapper.Map<List<UserDto>>(response.Data?.Data);
			return await Result<List<UserDto>>.SuccessAsync(mappedUser, "succeeded.");
		}
		catch (Exception ex)
		{
			throw ex.With("Failed to get user data from api!", ex.Message);
		}
	}
}