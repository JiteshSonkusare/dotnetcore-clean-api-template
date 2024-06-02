using Shared.Wrapper;
using System.Net.Mime;
using Domain.Entities;
using Domain.Configs.User;
using Shared.ApiClientHanlder;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Application.Features.Users.Dtos;
using Application.Interfaces.Repositories;
using Application.Common.ExceptionHandlers;

namespace Infrastructure.Respositories;

public class UserRepository : IUserRepository
{
	private readonly IRepositoryAsync<User, Guid> _repository;
	private readonly IApiClientWrapper _apiClientWrapper;
	private readonly UserConfig _userConfig;

	public UserRepository(IRepositoryAsync<User, Guid> repository, IApiClientWrapper apiClientWrapper, IOptions<UserConfig> userConfig)
	{
		_repository = repository;
		_apiClientWrapper = apiClientWrapper;
		_userConfig = userConfig.Value;
	}

	public async Task<bool> IsUserIdUniqueAsync(string? userId)
	{
		try
		{
			return await _repository.Entities.AnyAsync(b => b.UserId.Equals(userId));
		}
		catch (Exception ex)
		{
			throw ex.With(ex.Message, ex.Source, ex.InnerException?.Message, ex.StackTrace)
					.DetailData(nameof(userId), userId ?? string.Empty);
		}
	}

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