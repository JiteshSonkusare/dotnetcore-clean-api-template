using MediatR;
using Shared.Wrapper;
using Domain.Entities;
using Application.Resources.Constants;
using Application.Interfaces.Repositories;

namespace Application.Features.Users.Commands.DeleteUser;

public class DeleteUserCommand : IRequest<Result<Guid>>
{
    public Guid Id { get; set; }
}

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result<Guid>>
{
    private readonly IUnitOfWork<Guid> _unitOfWork;

    public DeleteUserCommandHandler(IUnitOfWork<Guid> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Repository<User>().GetByIdAsync(request.Id);
        if (user != null)
        {
            await _unitOfWork.Repository<User>().DeleteAsync(user);
            await _unitOfWork.CommitAndRemoveCacheAsync(cancellationToken, CacheConstants.UsersCacheKey);
            return await Result<Guid>.SuccessAsync(user.Id, "User Deleted");
        }
        else
        {
            return await Result<Guid>.FailAsync($"User Not Found with id: {request.Id}!");
        }
    }
}