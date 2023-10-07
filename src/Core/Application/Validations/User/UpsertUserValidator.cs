using FluentValidation;
using Application.Interfaces.Repositories;
using Application.Features.Users.Commands.UpsertUser;

namespace Application.Validations.User;

public class UpsertUserValidator : AbstractValidator<UpsertUserCommand>
{
    public UpsertUserValidator(IUserRepository userRepository)
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
            .WithMessage("User should not be null/empty.")
            .MustAsync(async (userId, cancellation) =>
            {
                bool exists = await userRepository.UserIdExists(userId);
                return !exists;
            }).WithMessage("User Id must be unique.");
    }
}
