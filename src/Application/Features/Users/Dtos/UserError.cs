using Shared.Wrapper;

namespace Application.Features.Users.Dtos;

public static class UserError
{
	public static readonly Error NotFound = new(
		"Users.NotFound", "Users not found!");

	public static Error NotFoundWithId(Guid id) => new(
		"User.NotFoundWithId", $"The user with Id '{id}' was not found");
}