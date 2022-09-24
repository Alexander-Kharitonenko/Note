using AutoMapper;
using Notes.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries.Users
{
    public class UserModelMapping : Profile
    {
        public UserModelMapping() 
        {
            CreateMap<User, UserModel>();
        }
    }
}
