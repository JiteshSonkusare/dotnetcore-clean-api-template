using FluentValidation;
using Application.Features.Users.Commands.UpsertUser;

namespace Application.Validations;

public class UserValidator : AbstractValidator<UpsertUserCommand>
{
    public UserValidator()
    {
        RuleFor(u => u.UserId)
            .NotEmpty()
            .WithMessage("UserId should not be null/empty.");

        RuleFor(u => u.FirstName)
            .NotEmpty()
            .WithMessage("FirstName should not be null/empty.");

        RuleFor(u => u.LastName)
            .NotEmpty()
            .WithMessage("LastName should not be null/empty.");

        RuleFor(u => u.Mobile)
            .NotEmpty()
            .WithMessage("Mobile should not be null/empty.");
    }
}
