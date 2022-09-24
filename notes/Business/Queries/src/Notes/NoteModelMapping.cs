using AutoMapper;
using Notes.Domain.Notes;
using Queries.Notes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries.src.Notes
{
    public class NoteModelMapping : Profile
    {
        public NoteModelMapping() 
        {
            CreateMap<Note, NoteModel>()
               .ForMember(noteModel => noteModel.Detailse,
                   opt => opt.MapFrom(note => note.Detailse))
               .ForMember(noteModel => noteModel.Titel,
                   opt => opt.MapFrom(note => note.Titel))
               .ForMember(noteModel => noteModel.Id,
                   opt => opt.MapFrom(note => note.Id))
               .ForMember(noteModel => noteModel.CreateDate,
                   opt => opt.MapFrom(note => note.CreateDate))
               .ForMember(noteModel => noteModel.EditTame,
                   opt => opt.MapFrom(note => note.EditTame));
        }
    }
}
