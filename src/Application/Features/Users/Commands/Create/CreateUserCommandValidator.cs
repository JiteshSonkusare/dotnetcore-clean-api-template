using FluentValidation;
using Application.Interfaces.Repositories;

namespace Application.Features.Users.Commands.Create;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
	public CreateUserCommandValidator(IUserRepository userRepository)
	{
		RuleFor(u => u.FirstName)
			.NotEmpty()
			.WithMessage("FirstName should not be null/empty.");

		RuleFor(u => u.LastName)
			.NotEmpty()
			.WithMessage("LastName should not be null/empty.");

		RuleFor(u => u.Mobile)
			.NotEmpty()
			.WithMessage("Mobile should not be null/empty.");

		RuleFor(c => c.UserId)
			.NotEmpty()
			.WithMessage("UserId should not be null/empty.")
			.MustAsync(async (userId, cancellationToken) =>
			{
				bool isUnique = await userRepository.IsUserIdUniqueAsync(userId);
				return !isUnique;
			})
			.WithMessage("UserId must be unique.");
	}
}
