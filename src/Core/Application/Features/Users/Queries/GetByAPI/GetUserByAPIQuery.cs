using MediatR;
using Shared.Wrapper;
using Application.Interfaces.Repositories;

namespace Application.Features.Users.Queries.GetByAPI;

public class GetUserByAPIQuery : IRequest<Result<string>>
{
    public GetUserByAPIQuery() { }
}

internal class GetUserByAPIQueryHandler : IRequestHandler<GetUserByAPIQuery, Result<string>>
{
    private readonly IUserRepository _userRepository;

    public GetUserByAPIQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<string>> Handle(GetUserByAPIQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _userRepository.GetUsersFromApiCall(cancellationToken);
            if (response.Data != null)
                return await Result<string>.SuccessAsync(response.Data);
            else
                return await Result<string>.FailAsync("No user found.");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}