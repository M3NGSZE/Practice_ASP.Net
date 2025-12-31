using FluentValidation;
using SuperHeroAPI_DotNet6.Models.Requests;

namespace SuperHeroAPI_DotNet6.Validators
{
    public class CreateUserRequestValidator : AbstractValidator<UserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Please provide a valid email address.");

            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("Username is required.")
                .Length(3, 100)
                .WithMessage("Username must be between 3 and 100 characters.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .MinimumLength(8)
                .WithMessage("Password must be at least 8 characters long.")
                .Matches(@"[A-Z]")
                .WithMessage("Password must contain at least one uppercase letter.")
                .Matches(@"[a-z]")
                .WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"[0-9]")
                .WithMessage("Password must contain at least one number.")
                .Matches(@"[@$!%*?&]")
                .WithMessage("Password must contain at least one special character (@$!%*?&).");
        }
    }
}
