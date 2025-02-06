using MediatR;
using AutoMapper;
using Shared.Wrapper;
using Domain.Entities;
using Application.Resources.Constants;
using Application.Interfaces.Repositories;
using Application.Common.ExceptionHandlers;

namespace Application.Features.Users.Commands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<Guid>>
{
	private readonly IUnitOfWork<Guid> _unitOfWork;
	private readonly IMapper _mapper;

	public CreateUserCommandHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<Result<Guid>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
	{
		try
		{
			var entity = _mapper.Map<User>(command);
			await _unitOfWork.Repository<User>().AddAsync(entity);
			await _unitOfWork.CommitAndRemoveCacheAsync(cancellationToken, CacheConstants.UserCacheKey);
			return Result.Create(entity.Id);
		}
		catch (Exception ex)
		{
			throw ex.With(ex.Message);
		}
	}
}