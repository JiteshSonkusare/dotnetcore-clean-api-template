using MediatR;
using AutoMapper;
using Shared.Wrapper;
using Domain.Entities;
using Application.Interfaces.Repositories;
using Application.Features.Users.Queries.ViewModels;

namespace Application.Features.Users.Queries.GetAll;

public class GetAllUserQuery : IRequest<Result<List<UserViewModel>>>
{
    public GetAllUserQuery() { }
}

internal class GetUserQueryHandler : IRequestHandler<GetAllUserQuery, Result<List<UserViewModel>>>
{
    private readonly IUnitOfWork<Guid> _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserQueryHandler(IUnitOfWork<Guid> unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<UserViewModel>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        //var failures = new FailureResponse() { Type = "test_type", Status = "404", Error = "test_error", ErrorDescription = "test_error_description" };

        //throw new Exception(failures.ToString());

        try
        {
            var user = await _unitOfWork.Repository<User>().GetAllAsync();
            if (user.Count == 0)
                return await Result<List<UserViewModel>>.FailAsync("No users found!");
            var mappedUser = _mapper.Map<List<UserViewModel>>(user);
            return await Result<List<UserViewModel>>.SuccessAsync(mappedUser);
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to get user data! Error: {ex.Message}");
        }
    }
}
