using AutoMapper;
using Queries.Users;

namespace Note.API.View.Auth
{
    public class ViewLoginModelMapping : Profile
    {
        public ViewLoginModelMapping() 
        {
            CreateMap<UserModel, ViewLoginModel>()
                .ForMember(loginModel => loginModel.Email,
              opt => opt.MapFrom(user => user.Email))
                .ForMember(loginModel => loginModel.Password,
              opt => opt.MapFrom(user => user.Password));
        }

    }
}
