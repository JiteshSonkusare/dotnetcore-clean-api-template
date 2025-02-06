using Shared.Wrapper;
using System.Net.Mime;
using Domain.ViewModels;
using Domain.Configs.User;
using Shared.ApiClientHanlder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Application.Features.Users.Dtos;
using Application.Interfaces.Services;
using Application.Common.ExceptionHandlers;

namespace Infrastructure.Services;

public class UserService(
    IOptions<UserConfig> userConfig,
    IApiClientWrapper apiClientWrapper)
    : IUserService
{
    private readonly UserConfig _userConfig = userConfig.Value;
    private readonly IApiClientWrapper _apiClientWrapper = apiClientWrapper;

    public async Task<Result<List<UserDto>>> GetUsersFromApiCall(CancellationToken cancellation)
    {
        try
        {
            var result = await _apiClientWrapper.Send(
                new RequestParameters
                {
                    RequestUri = new Uri($"{_userConfig?.BaseURL}/users" ?? string.Empty),
                    Method = HttpMethod.Get,
                    AuthHandler = null,
                    RequestHeaders = Array.Empty<HeaderData>(),
                    DefaultRequestHeaders = new DefaultRequestHeaders(new string[] { MediaTypeNames.Application.Json }, Array.Empty<HeaderData>()),
                    RequestContent = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("", "") })
                },
                cancellation);

            if (result.StatusCode == StatusCodes.Status200OK)
            {
                var response = new Response<ApiResponse<List<UserDto>>>(result);
                if (response.Data != null && response.Data.Suceeded)
                    return Result.Success(response?.Data?.Data ?? []);
                else
                    return Result.Failure<List<UserDto>>(new Error(response?.Data?.Error?.Code ?? string.Empty, response?.Data?.Error?.Message ?? string.Empty));
            }
            else
                return Result.Failure<List<UserDto>>(new Error("Failed", "Null response received"));
        }
        catch (Exception ex)
        {
            throw ex.With();
        }
    }
}
