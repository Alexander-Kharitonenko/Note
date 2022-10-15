using FluentValidation;
using FluentValidation.Validators;

namespace Note.API.View.Auth
{
    public class ViewRefreshTokenModelValidator : AbstractValidator<ViewRefreshTokenModel>
    {
        public ViewRefreshTokenModelValidator()
        {
            RuleFor(p => p.Email).EmailAddress(EmailValidationMode.AspNetCoreCompatible);
        }
    }
}
