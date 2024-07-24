using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces.Repositories;
using Application.Common.ExceptionHandlers;

namespace Infrastructure.Respositories;

public class UserRepository(
	IRepositoryAsync<User, Guid> repository) 
	: IUserRepository
{
	private readonly IRepositoryAsync<User, Guid> _repository = repository;

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
}