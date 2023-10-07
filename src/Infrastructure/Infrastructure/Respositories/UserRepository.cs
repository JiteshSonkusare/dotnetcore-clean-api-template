using System.Net.Mime;
using Domain.Entities;
using Domain.ViewModels;
using Domain.Configs.User;
using Shared.ApiClientHanlder;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces.Repositories;
using Application.Features.Users.Queries.ViewModels;
using Shared.Wrapper;

namespace Infrastructure.Respositories;

public class UserRepository : IUserRepository
{
    private readonly UserConfig _userConfig;
    private readonly IApiClientWrapper _apiClientWrapper;
    private readonly IRepositoryAsync<User, Guid> _repository;

    public UserRepository(IRepositoryAsync<User, Guid> repository, IApiClientWrapper apiClientWrapper, UserConfig userConfig)
    {
        _repository = repository;
        _apiClientWrapper = apiClientWrapper;
        _userConfig = userConfig;
    }

    public async Task<bool> UserIdExists(string? userId)
    {
        return await _repository.Entities.AnyAsync(b => b.UserId == userId);
    }

    public async Task<Response<Result<List<UserViewModel>>>> GetUsersFromApiCall(CancellationToken cancellation)
    {
        try
        {
            var result = await _apiClientWrapper.Send(
                new RequestParameters
                {
                    RequestUri = new Uri(_userConfig?.BaseURL ?? string.Empty),
                    Method = HttpMethod.Get,
                    AuthHandler = null,
                    RequestHeaders = Array.Empty<HeaderData>(),
                    RequestContent = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("", "") }),
                    DefaultRequestHeaders = new DefaultRequestHeaders(new string[] { MediaTypeNames.Application.Json }, Array.Empty<HeaderData>())
                },
                cancellation);

            return new Response<Result<List<UserViewModel>>>(result, result.StatusCode);
        }
        catch (Exception ex)
        {
            var failure = new FailureResponse { Source = ex.Source, Error = ex.Message, ErrorDescription = ex.InnerException?.Message };
            throw new Exception(failure.ToString());
        }
    }
}