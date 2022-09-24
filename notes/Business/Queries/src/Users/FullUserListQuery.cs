using Notes.Environment.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Notes.Domain.Users;
using System.Linq.Expressions;
using Microsoft.AspNetCore.OData.Query;

namespace Queries.Users
{
    public class FullUserListQuery : IRequest<SelectState<UserModel>>
    {
        public ODataQueryOptions<UserModel> options { get; set; } 
    }
}
