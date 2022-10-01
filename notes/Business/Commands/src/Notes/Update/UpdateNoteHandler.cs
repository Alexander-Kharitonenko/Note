
using Commands.Notes.Delete;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Domain.Interfaces;
using Notes.Environment.Commands;
using Notes.Environment.SystemConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Commands.Notes.Update
{
    public class UpdateNoteHandler : IRequestHandler<UpdateNoteCommand, CommandState>
    {
        private readonly IAppDbContext _context;
        private readonly IValidator<UpdateNoteCommand> _validator;

        public UpdateNoteHandler(IAppDbContext context, IValidator<UpdateNoteCommand> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<CommandState> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {
            if (!_validator.Validate(request).IsValid) 
            {
                var errorMessage = string.Join('\n', _validator.Validate(request).Errors.Where(el => el != null));
                CommandState.Error(StatusCode.InvalidData, errorMessage);
            }

            var note = await _context.Notes.FirstOrDefaultAsync(el => el.Id == request.Id, cancellationToken);

            if (note == null)
            {
                return CommandState.Error(StatusCode.NoteNotFound);
            }

            note.Titel = request.Title;
            note.Detailse = request.Details;
            note.IsCmpleted = request.isCompleted;
            note.EditTame = DateTime.UtcNow;

            var result = _context.Notes.Update(note);
            var saveResulr = await _context.SaveChangesAsync(cancellationToken);

            if (saveResulr == 0)
            {
                return CommandState.Error(StatusCode.NoteNotUpdated);
            }

            return CommandState.Success(result.Entity.Id);
        }
    }
}
