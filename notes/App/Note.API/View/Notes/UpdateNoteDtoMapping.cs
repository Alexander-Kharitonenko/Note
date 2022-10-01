using AutoMapper;
using Commands.Notes.Update;

namespace Note.API.View.Notes
{
    public class UpdateNoteDtoMapping : Profile
    {
        public UpdateNoteDtoMapping() 
        {
            CreateMap<UpdateNoteDto, UpdateNoteCommand>()
                .ForMember(el => el.isCompleted,
                    opt => opt.MapFrom(dto => dto.isCompleted))
                .ForMember(el => el.Title,
                    opt => opt.MapFrom(dto => dto.Title))
                .ForMember(el => el.Details,
                    opt => opt.MapFrom(dto => dto.Details));
        }
    }
}
