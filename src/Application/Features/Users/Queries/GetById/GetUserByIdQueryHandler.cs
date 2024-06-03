using MediatR;
using AutoMapper;
using Shared.Wrapper;
using Domain.Entities;
using Application.Features.Users.Dtos;
using Application.Interfaces.Repositories;
using Application.Common.ExceptionHandlers;

namespace Application.Features.Users.Queries.GetById;

internal class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
{
	private readonly IUnitOfWork<Guid> _unitOfWork;
	private readonly IMapper _mapper;

	public GetUserByIdQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var user = await _unitOfWork.Repository<User>().GetByIdAsync(request.Id);
			if (user == null)
				return Result.Failure<UserDto>(UserError.NotFoundWithId(request.Id));
			var mappedUser = _mapper.Map<UserDto>(user);
			return Result.Success(mappedUser);
		}
		catch (Exception ex)
		{
			throw ex.With($"Failed to get user data with id: {request.Id}! Error: {ex.Message}");
		}
	}
}