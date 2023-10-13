using MediatR;
using AutoMapper;
using Shared.Wrapper;
using Application.Interfaces.Repositories;
using Application.Features.Users.Queries.ViewModels;

namespace Application.Features.Users.Queries.GetByAPI;

public class GetUserByAPIQuery : IRequest<Result<List<UserViewModel>>>
{
    public GetUserByAPIQuery() { }
}

internal class GetUserByAPIQueryHandler : IRequestHandler<GetUserByAPIQuery, Result<List<UserViewModel>>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    
    public GetUserByAPIQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<UserViewModel>>> Handle(GetUserByAPIQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _userRepository.GetUsersFromApiCall(cancellationToken);
            if (response.Data?.Data == null)
                return await Result<List<UserViewModel>>.FailAsync("No users found!");
            var mappedUser = _mapper.Map<List<UserViewModel>>(response.Data?.Data);
            return await Result<List<UserViewModel>>.SuccessAsync(mappedUser, "succeeded.");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}