using Commands.Notes.Create;
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

namespace Commands.Notes.Delete
{
    public class DeleteNoteHandler : IRequestHandler<DeleteNoteCommand, CommandState>
    {
        private readonly IAppDbContext _context;
        private readonly IValidator<DeleteNoteCommand> _validator;
        public DeleteNoteHandler(IAppDbContext context, IValidator<DeleteNoteCommand> validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<CommandState> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
        {
            if (!_validator.Validate(request).IsValid) 
            {
                var errorMessage = string.Join('\n', _validator.Validate(request).Errors.Where(el => el != null));
                return CommandState.Error(StatusCode.InvalidData, errorMessage);
            }

            var note = await _context.Notes.FirstOrDefaultAsync(el => el.Id == request.Id);

            if (note == null)
            {
                return CommandState.Error(StatusCode.NoteNotFound);
            }

            var result = _context.Notes.Remove(note);
            var saveResulr = await _context.SaveChangesAsync(cancellationToken);

            if (saveResulr == 0)
            {
                return CommandState.Error(StatusCode.NoteNotDeleted);
            }

            return CommandState.Success(result.Entity.Id);
        }
    }
}
