using FluentValidation;

namespace Note.API.View.Notes
{
    public class CreateNoteDtoValidator : AbstractValidator<CreateNoteDto>
    {
        CreateNoteDtoValidator() 
        {
            RuleFor(p => p.Title).NotEmpty();
            RuleFor(p => p.Details).NotEmpty();
        }
    }
}
