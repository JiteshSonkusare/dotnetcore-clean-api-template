using Shared.Wrapper;

namespace Application.Features.Users.Dtos;

public static class UserError
{
	public static readonly Error NotFound = new(
		"User.NotFound", "Users not found!");

	public static Error NotFoundWithId(Guid id) => new(
		"User.NotFound.Id", $"The user with Id '{id}' was not found");
}