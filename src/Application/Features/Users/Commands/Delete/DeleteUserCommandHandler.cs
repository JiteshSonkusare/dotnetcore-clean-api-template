using MediatR;
using Shared.Wrapper;
using Domain.Entities;
using Application.Common.Exceptions;
using Application.Resources.Constants;
using Application.Features.Users.Dtos;
using Application.Interfaces.Repositories;
using Application.Common.ExceptionHandlers;

namespace Application.Features.Users.Commands;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<Guid>>
{
	private readonly IUnitOfWork<Guid> _unitOfWork;

	public DeleteUserCommandHandler(IUnitOfWork<Guid> unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<Guid>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var user = await _unitOfWork.Repository<User>().GetByIdAsync(request.Id);
			if (user != null)
			{
				await _unitOfWork.Repository<User>().DeleteAsync(user);
				await _unitOfWork.CommitAndRemoveCacheAsync(cancellationToken, CacheConstants.UserCacheKey);
				return await Result<Guid>.SuccessAsync(request.Id, "User Deleted");
			}
			else
			{
				return await Result<Guid>.FailureAsync(UserError.NotFoundWithId(request.Id));
			}
		}
		catch (Exception ex)
		{
			throw ex.With("User Deletion Failed!", ex.Message, ex.Source, ex.InnerException?.Message, ex.StackTrace)
					.DetailData(nameof(request.Id), request.Id);
		}
	}
}