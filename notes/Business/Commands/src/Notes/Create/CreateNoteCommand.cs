
using MediatR;
using Notes.Environment.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands.Notes.Create
{
    public class CreateNoteCommand : IRequest<CommandState>
    {
        public string Title { get; set; }
        public string Details { get; set; }
    }
}
