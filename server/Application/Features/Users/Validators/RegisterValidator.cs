using Application.Features.Users.Dto;
using FluentValidation;

namespace Application.Features.Users.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username can't be empty");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email can't be empty")
                .EmailAddress().WithMessage("Email isn't valid.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password can't be empty")
                .MinimumLength(4).WithMessage("Password must be between 4-8 characters")
                .MaximumLength(8).WithMessage("Password must be between 4-8 characters")
                    .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                    .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                    .Matches(@"\d+").WithMessage("Your password must contain at least one number.");
            RuleFor(x => x.DisplayName)
                .NotEmpty().WithMessage("Display name can't be empty");
        }
    }
}