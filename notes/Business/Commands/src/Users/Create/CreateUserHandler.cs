using AutoMapper;
using Commands.Users.Create;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Domain.Interfaces;
using Notes.Domain.Users;
using Notes.Environment.Commands;
using Notes.Environment.SystemConstants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Commands.Users.Create
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, CommandState>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateUserCommand> _validator;
        public CreateUserHandler(IAppDbContext context, IMapper mapper, IValidator<CreateUserCommand> validator) 
        {
            _context = context;
            _mapper = mapper;
            _validator = validator; 
        }
        public async Task<CommandState> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (!_validator.Validate(request).IsValid)
            {
                var errorMessage = string.Join('\n', _validator.Validate(request).Errors.Where(el => el != null));
                CommandState.Error(StatusCode.InvalidData, errorMessage);
            }

            var target = await _context.Users.FirstOrDefaultAsync(el => el.Email == request.Email || el.Login == request.Login);

            if (target != null) 
            {
               return CommandState.Error(StatusCode.DuplicationResul);
            }

            var user = new User()
            {
                Login = request.Login,
                Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(request.Password)),
                Email = request.Email
            };

            var result = await _context.Users.AddAsync(user, cancellationToken);
            var saveResulr = await _context.SaveChangesAsync(cancellationToken);

            if (saveResulr == 0)
            {
                return CommandState.Error(StatusCode.UserNotCreated);
            }

            return CommandState.Success(result.Entity.Id);
        }
    }
}
