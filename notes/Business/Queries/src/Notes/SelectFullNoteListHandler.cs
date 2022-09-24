using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Domain.Interfaces;
using Notes.Domain.Notes;
using Notes.Environment.Queries;
using Notes.Environment.SystemConstants;
using Queries.Notes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Queries.Notes
{
    public class SelectFullNoteListHandler : IRequestHandler<FullNoteListQuery, SelectState<NoteModel>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;
        public SelectFullNoteListHandler(IAppDbContext context, IMapper mapper) 
        {
            _context = context; 
            _mapper = mapper;
        }
        public async Task<SelectState<NoteModel>> Handle(FullNoteListQuery request, CancellationToken cancellationToken)
        {

            IEnumerable<NoteModel> notes = null;

            var data = _context.Notes.Select(el => el).ProjectTo<NoteModel>(_mapper.ConfigurationProvider);

            notes = await request.options.ApplyTo(data).Cast<NoteModel>().ToListAsync(cancellationToken);


            if (notes == null) 
            {
               return SelectState<NoteModel>.Error(StatusCode.NoteNotFound);
            }

            return SelectState<NoteModel>.Success(notes);
        }
    }
}
