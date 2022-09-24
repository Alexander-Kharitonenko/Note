
using MediatR;
using Notes.Environment.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands.Notes.Delete
{
    public class DeleteNoteCommand : IRequest<CommandState>
    {
        public ulong Id { get; set; }
    }
}
