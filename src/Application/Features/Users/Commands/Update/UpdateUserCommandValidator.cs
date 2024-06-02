using FluentValidation;
using Application.Interfaces.Repositories;

namespace Application.Features.Users.Commands;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
	public UpdateUserCommandValidator(IUserRepository userRepository)
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

		RuleFor(u => u.UserId)
			.NotEmpty()
			.WithMessage("UserId should not be null/empty.")
			.MustAsync(async (model, userId, cancellation) =>
			{
				if (model.Id != Guid.Empty)
				{
					bool exists = await userRepository.IsUserIdUniqueAsync(userId);
					return !exists;
				}
				return true;
			})
			.WithMessage("User Id must be unique.");
	}
}
