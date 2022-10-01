using AutoMapper;
using Commands.Notes.Create;

namespace Note.API.View.Notes
{
    public class CreateNoteDtoMapping : Profile
    {
        public CreateNoteDtoMapping() 
        {
            CreateMap<CreateNoteDto, CreateNoteCommand>()
               .ForMember(el => el.Title,
                    opt => opt.MapFrom(dto => dto.Title))
               .ForMember(command => command.Details,
                    opt => opt.MapFrom(dto => dto.Details))
               .ForMember(el => el.UserId, 
                    opt => opt.MapFrom(dto => dto.UserId));
        }
    }
}
