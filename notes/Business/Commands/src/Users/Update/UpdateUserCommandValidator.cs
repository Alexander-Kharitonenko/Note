using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands.Users.Update
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator() 
        {
            RuleFor(p => p.Login).Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MinimumLength(6);

            RuleFor(p => p.Password).Cascade(CascadeMode.Stop)
               .NotEmpty()
               .MinimumLength(6);

            RuleFor(p => p.Email).Cascade(CascadeMode.Stop)
             .NotEmpty()
             .EmailAddress(EmailValidationMode.AspNetCoreCompatible);
        }
    }
}
