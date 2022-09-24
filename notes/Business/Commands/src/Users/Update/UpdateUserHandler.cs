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

namespace Commands.Users.Update
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, CommandState>
    {
        private readonly IAppDbContext _context;
        private readonly IValidator<UpdateUserCommand> _validator;

        public UpdateUserHandler(IAppDbContext context, IValidator<UpdateUserCommand> validator) 
        {
            _context = context;
            _validator = validator;
        }

        public async Task<CommandState> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (!_validator.Validate(request).IsValid)
            {
                var errorMessage = string.Join('\n', _validator.Validate(request).Errors.Where(el => el != null));
                CommandState.Error(StatusCode.InvalidData, errorMessage);
            }

            var user = await _context.Users.FirstOrDefaultAsync(el => el.Id == request.UserId);

            if (user == null) 
            {
                return CommandState.Error(StatusCode.NoteNotFound);
            }

            user.Login = request.Login;
            user.Email = request.Email;
            user.Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(request.Password));

            var result = _context.Users.Update(user);
            var saveResulr = await _context.SaveChangesAsync(cancellationToken);

            if (saveResulr == 0)
            {
                return CommandState.Error(StatusCode.UserNotUpdated);
            }

            return CommandState.Success(result.Entity.Id);
        }
    }
}
