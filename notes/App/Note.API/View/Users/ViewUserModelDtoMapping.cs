using AutoMapper;
using Queries.Users;

namespace Note.API.View.Users
{
    public class ViewUserModelDtoMapping : Profile
    {
        public ViewUserModelDtoMapping() 
        {
            CreateMap<UserModel, ViewUserModelDto>()
                .ForMember(dto => dto.Login,
                opt => opt.MapFrom(model => model.Login))
                .ForMember(dto => dto.Password,
                opt => opt.MapFrom(model => model.Password))
                .ForMember(dto => dto.Email,
                opt => opt.MapFrom(model => model.Email))
                .ForMember(dto => dto.id, 
                opt => opt.MapFrom(model => model.Id));
        }
    }
}
