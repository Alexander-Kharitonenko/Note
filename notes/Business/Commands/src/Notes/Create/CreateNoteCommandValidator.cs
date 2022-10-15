using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands.Notes.Create
{
    public class CreateNoteCommandValidator : AbstractValidator<CreateNoteCommand>
    {
        public CreateNoteCommandValidator() 
        {
            //TODO: добавить нормальную валидацию полей
            RuleFor(x => x.Title)
                .NotEmpty()
                .MinimumLength(1);

            RuleFor(x => x.Details)
                .NotEmpty()
                .MinimumLength(5);
        }
    }
}
