using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Environment.Queries;
using Notes.Environment.SystemConstants;
using Notes.Domain.Interfaces;
using Notes.Domain.Notes;
using Queries.Notes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Queries.Notes
{
    public class FindNoteHandler : IRequestHandler<FindNoteQuery, QueryState<NoteModel>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        public FindNoteHandler(IAppDbContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<QueryState<NoteModel>> Handle(FindNoteQuery request, CancellationToken cancellationToken)
        {
            var note = await _context.Notes.FirstOrDefaultAsync(el => el.Id == request.Id, cancellationToken);

            if (note == null)
            {
                return QueryState<NoteModel>.Error(StatusCode.NoteNotFound);
            }

            var data = _mapper.Map<NoteModel>(note);

            return  QueryState<NoteModel>.Success(data);
        }
    }
}
