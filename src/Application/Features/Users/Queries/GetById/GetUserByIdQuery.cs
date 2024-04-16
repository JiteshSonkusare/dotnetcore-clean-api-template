using MediatR;
using AutoMapper;
using Shared.Wrapper;
using Domain.Entities;
using Application.Interfaces.Repositories;
using Application.Features.Users.Queries.ViewModels;
using Application.Common.Exceptions;

namespace Application.Features.Users.Queries.GetById;

public class GetUserByIdQuery : IRequest<Result<UserViewModel>>
{
    public Guid Id { get; set; }
}

internal class GetUserQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserViewModel>>
{
    private readonly IUnitOfWork<Guid> _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<UserViewModel>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _unitOfWork.Repository<User>().GetByIdAsync(request.Id);
            if (user == null) 
                return await Result<UserViewModel>.FailAsync($"User Not Found with id: {request.Id}");
            var mappedUser = _mapper.Map<UserViewModel>(user);
            return await Result<UserViewModel>.SuccessAsync(mappedUser);
        }
        catch (Exception ex)
        {
            throw new GeneralApplicationException($"Failed to get user data with id: {request.Id}! Error: {ex.Message}");
        }
    }
}