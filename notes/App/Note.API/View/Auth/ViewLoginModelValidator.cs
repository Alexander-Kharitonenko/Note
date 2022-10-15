using FluentValidation;
using FluentValidation.Validators;
using System.Runtime.CompilerServices;

namespace Note.API.View.Auth
{
    public class ViewLoginModelValidator : AbstractValidator<ViewLoginModel>
    {
        public ViewLoginModelValidator() 
        {
            //TODO: добавить нормальную валидация полей
            RuleFor(p => p.Email).EmailAddress(EmailValidationMode.AspNetCoreCompatible);
            RuleFor(p => p.Password).NotEmpty();
        }

    }
}
