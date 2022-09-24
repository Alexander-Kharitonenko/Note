using AutoMapper;
using Note.API.View.Users;
using Notes.Domain.Users;

namespace Note.API.View.Auth
{
    public class ViewRegisterModelMapping : Profile
    {
        public ViewRegisterModelMapping() 
        {
            CreateMap<ViewRegisterModel, User>()
                .ForMember(view => view.Email, 
                   opt => opt.MapFrom(model => model.Email))
                .ForMember(view => view.Password,
                   opt => opt.MapFrom(model => model.Password))
                .ForMember(view => view.Login,
                   opt => opt.MapFrom(model => model.Login));
        }
    }
}
