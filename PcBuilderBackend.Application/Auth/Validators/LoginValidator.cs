using FluentValidation;
using PcBuilderBackend.Application.Auth.Dtos;

namespace PcBuilderBackend.Application.Auth.Validators
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
