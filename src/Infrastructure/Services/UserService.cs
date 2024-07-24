using Shared.Wrapper;
using System.Net.Mime;
using Domain.Configs.User;
using Shared.ApiClientHanlder;
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

	public async Task<Response<Result<List<UserDto>>>> GetUsersFromApiCall(CancellationToken cancellation)
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

			return new Response<Result<List<UserDto>>>(result, false, result.StatusCode);
		}
		catch (Exception ex)
		{
			throw ex.With();
		}
	}
}
