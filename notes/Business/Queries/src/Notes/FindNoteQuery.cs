using Notes.Environment.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries.Notes
{
    public class FindNoteQuery : IRequest<QueryState<NoteModel>>
    {
        public ulong UserId { get; set; }
        public ulong Id { get; set; }
    }
}
