using Notes.Environment.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands.Users.Delete
{
    public class DeleteUserCommand : IRequest<CommandState>
    {
        public ulong Id { get; set; }
    }
}
