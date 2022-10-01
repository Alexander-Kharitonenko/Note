using Notes.Environment.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commands.Notes.Update
{
    public class UpdateNoteCommand : IRequest<CommandState>
    {
        public ulong Id { get; set; }
        public ulong UserId { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public bool isCompleted { get; set; }
        public DateTime? EditTame { get; set; }
    }
}
