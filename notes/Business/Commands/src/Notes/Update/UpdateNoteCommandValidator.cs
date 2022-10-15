using Commands.Notes.Update;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands.Notes.Update
{
    public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
    {
        public UpdateNoteCommandValidator() 
        {
            //TODO: добавить нормальную валидацию полей

            RuleFor(p => p.Title)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MinimumLength(1);

        }
    }
}
