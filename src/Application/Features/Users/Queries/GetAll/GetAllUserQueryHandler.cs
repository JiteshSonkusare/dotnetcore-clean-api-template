using MediatR;
using LazyCache;
using AutoMapper;
using Shared.Wrapper;
using Domain.Entities;
using Application.Features.Users.Dtos;
using Application.Resources.Constants;
using Application.Interfaces.Repositories;
using Application.Common.ExceptionHandlers;

namespace Application.Features.Users.Queries.GetAll;

internal class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, Result<List<UserDto>>>
{
	private readonly IUnitOfWork<Guid> _unitOfWork;
	private readonly IMapper _mapper;
	private readonly IAppCache _cache;

	public GetAllUserQueryHandler(IMapper mapper, IUnitOfWork<Guid> unitOfWork, IAppCache cache)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
		_cache = cache;
	}

	public async Task<Result<List<UserDto>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
	{
		try
		{
			Task<List<User>> getAllUsers() => _unitOfWork.Repository<User>().GetAllAsync();
			var users = await _cache.GetOrAddAsync(CacheConstants.UserCacheKey, getAllUsers);
			if (users.Count == 0)
				return Result.Failure<List<UserDto>>(UserError.NotFound);
			var mappedUser = _mapper.Map<List<UserDto>>(users);
			return Result.Success(mappedUser);
		}
		catch (Exception ex)
		{
			throw ex.With("Failed to get user data!", ex.Message);
		}
	}
}