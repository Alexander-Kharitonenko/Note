using MediatR;
using Notes.Environment.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands.Users.Update
{
    public class UpdateUserCommand : IRequest<CommandState>
    {
        public ulong UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
