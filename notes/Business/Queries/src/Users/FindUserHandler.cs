using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Environment.Queries;
using Notes.Environment.SystemConstants;
using Notes.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Queries.Users
{
    public class FindUserHandler : IRequestHandler<FindUserQuery, QueryState<UserModel>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public FindUserHandler(IAppDbContext context, IMapper mapper) 
        {
            _context = context;
            _mapper=mapper;
        }

        public async Task<QueryState<UserModel>> Handle(FindUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (user == null) 
            {
              return QueryState<UserModel>.Error(StatusCode.UserNotFound);
            }

            var data = _mapper.Map<UserModel>(user);

            return QueryState<UserModel>.Success(data);
        }
    }
}
