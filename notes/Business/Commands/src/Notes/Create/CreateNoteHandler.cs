using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Environment.Commands;
using Notes.Environment.SystemConstants;
using Notes.Domain.Interfaces;
using Notes.Domain.Notes;
using Notes.Persistence.MySQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;

namespace Commands.Notes.Create
{
    public class CreateNoteHandler : IRequestHandler<CreateNoteCommand, CommandState>
    {
        private readonly IAppDbContext _context;
        private readonly IValidator<CreateNoteCommand> _validator;
 
        public CreateNoteHandler(IAppDbContext context, IValidator<CreateNoteCommand> validator)
        {
            _context = context;
            _validator = validator;
        }
        public async Task<CommandState> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {

            if (!_validator.Validate(request).IsValid) 
            {
              
                var errorMessage = string.Join('\n', _validator.Validate(request).Errors.Where(el => el != null));
                return CommandState.Error(StatusCode.InvalidData, errorMessage);
            }

            var note = new Note()
            {
                UserId = 1,
                Titel = request.Title,
                Detailse = request.Details,
                CreateDate = DateTime.UtcNow,
                EditTame = null
            };

            var result = await _context.Notes.AddAsync(note);

            var saveResulr = await _context.SaveChangesAsync(cancellationToken);

            if (saveResulr == 0)
            {
                return CommandState.Error(StatusCode.NoteNotCreated);
            }

            return CommandState.Success(result.Entity.Id);

        }
    }
}
