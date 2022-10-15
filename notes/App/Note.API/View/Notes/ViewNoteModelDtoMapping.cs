using AutoMapper;
using Queries.Notes;

namespace Note.API.View.Notes
{
    public class ViewNoteModelDtoMapping : Profile
    {
        public ViewNoteModelDtoMapping()
        {
            CreateMap<NoteModel, ViewNoteModelDto>()
                .ForMember(noteModel => noteModel.Id,
                     opt => opt.MapFrom(note => note.Id))
                .ForMember(noteModel => noteModel.UserId, 
                     opt => opt.MapFrom(note => note.UserId))
                .ForMember(noteModel => noteModel.Titel,
                     opt => opt.MapFrom(note => note.Titel))
                 .ForMember(noteModel => noteModel.Detailse,
                     opt => opt.MapFrom(note => note.Detailse))
                 .ForMember(noteModel => noteModel.IsCmpleted,
                     opt => opt.MapFrom(note => note.IsCmpleted))
                 .ForMember(noteModel => noteModel.CreateDate,
                     opt => opt.MapFrom(note => note.CreateDate))
                 .ForMember(noteModel => noteModel.EditTame,
                     opt => opt.MapFrom(note => note.EditTame));
        }
    }
}
