using Commands.Users.Create;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands.Users.Create
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator() 
        {
            //TODO: добавить нормальную валидацию полей
            RuleFor(p => p.Email).Cascade(CascadeMode.Stop)
                .NotEmpty()
                .EmailAddress(EmailValidationMode.AspNetCoreCompatible);

            RuleFor(p => p.Password).Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MinimumLength(8);

            RuleFor(p => p.Login).Cascade(CascadeMode.Stop)
               .NotEmpty()
               .MinimumLength(8);
        }
    }
}
