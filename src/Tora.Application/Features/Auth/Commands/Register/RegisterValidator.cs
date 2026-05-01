using FluentValidation;

namespace Tora.Application.Features.Auth.Commands.Register;

public class RegisterValidator : AbstractValidator<RegisterCommand>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("valid email is required");
        RuleFor(x=> x.Password)
        .NotEmpty()
        .MinimumLength(8).WithMessage("Password must be atleast 8 cahracters long")
        .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
        .Matches("[0-9]").WithMessage("Password must contain at least one digit");
    }
}
