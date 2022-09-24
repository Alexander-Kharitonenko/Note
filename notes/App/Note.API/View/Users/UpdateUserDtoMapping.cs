using AutoMapper;
using Commands.Users.Update;

namespace Note.API.View.Users
{
    public class UpdateUserDtoMapping : Profile
    {
        public UpdateUserDtoMapping() 
        {
            CreateMap<UpdateUserDto, UpdateUserCommand>();
        }
    }
}
