using Notes.Environment.Queries;
using MediatR;
using Notes.Domain.Notes;
using Queries.Notes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.OData.Query;

namespace Queries.Notes
{
    public class FullNoteListQuery: IRequest<SelectState<NoteModel>>
    {
      public ODataQueryOptions<NoteModel> options { get; set; }
    }
}
