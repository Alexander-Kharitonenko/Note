using Notes.Environment.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries.Users
{
    public class FindUserQuery : IRequest<QueryState<UserModel>>
    {
        public ulong Id { get; set; }
    }
}
