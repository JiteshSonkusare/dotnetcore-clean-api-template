using MediatR;
using Shared.Wrapper;
using Domain.Entities;
using Application.Common.Exceptions;
using Application.Features.Users.Dtos;
using Application.Resources.Constants;
using Application.Interfaces.Repositories;
using Application.Common.ExceptionHandlers;

namespace Application.Features.Users.Commands;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<Guid>>
{
	private readonly IUnitOfWork<Guid> _unitOfWork;

	public UpdateUserCommandHandler(IUnitOfWork<Guid> unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Result<Guid>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var entity = await _unitOfWork.Repository<User>().GetByIdAsync(request.Id);
			if (entity == null)
				return await Result<Guid>.FailureAsync(UserError.NotFoundWithId(request.Id));
			else
			{
				entity.FirstName = request.FirstName ?? entity.FirstName;
				entity.LastName = request.LastName ?? entity.LastName;
				entity.UserId = request.UserId ?? entity.UserId;
				entity.Phone = request.Phone ?? entity.Phone;
				entity.Mobile = request.Mobile ?? entity.Mobile;
				entity.Address = request.Address ?? entity.Address;
				entity.Status = request.Status ?? entity.Status;

				await _unitOfWork.Repository<User>().UpdateAsync(entity);
				await _unitOfWork.CommitAndRemoveCacheAsync(cancellationToken, CacheConstants.UserCacheKey);
				return await Result<Guid>.SuccessAsync(request.Id, "User Updated Successfully!");
			}
		}
		catch (Exception ex)
		{
			throw ex.With("User Update Failed! Error", ex.Message, ex.Source, ex.InnerException?.Message, ex.StackTrace)
					.DetailData(nameof(request.Id), request.Id);
		}
	}
}