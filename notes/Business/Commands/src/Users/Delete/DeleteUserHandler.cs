using Commands.Users.Delete;
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

namespace Commands.Users.Delete
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, CommandState>
    {
        private readonly IAppDbContext _context;
        private readonly IValidator<DeleteUserCommand> _validator;
      
        public DeleteUserHandler(IAppDbContext context, IValidator<DeleteUserCommand> validator) 
        {
            _context = context;
            _validator = validator;
        }

        public async Task<CommandState> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            if (_validator.Validate(request).IsValid)
            {
                var errorMessage = string.Join('\n', _validator.Validate(request).Errors.Where(el => el != null));
                CommandState.Error(StatusCode.InvalidData, errorMessage);
            }

            var user = await _context.Users.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (user == null) 
            {
                CommandState.Error(StatusCode.NoteNotFound);
            }

            var resulr = _context.Users.Remove(user);
            var saveResulr = await _context.SaveChangesAsync(cancellationToken);

            if (saveResulr == 0) 
            {
                CommandState.Error(StatusCode.UserNotDeleted);
            }

            return CommandState.Success(resulr.Entity.Id);
        }
    }
}
