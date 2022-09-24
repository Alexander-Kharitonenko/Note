using AutoMapper;
using Commands.Users.Create;

namespace Note.API.View.Users
{
    public class CreateUserDtoMapping : Profile
    {
        public CreateUserDtoMapping() 
        {
            CreateMap<CreateUserDto, CreateUserCommand>();
        }
    }
}
