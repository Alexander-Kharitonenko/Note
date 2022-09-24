using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Environment.Queries;
using Notes.Environment.SystemConstants;
using Notes.Domain.Interfaces;
using Notes.Domain.Users;
using Queries.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.OData.Query;

namespace Queries.Users
{
    public class SelectFullUserListHandler : IRequestHandler<FullUserListQuery, SelectState<UserModel>>
    {
        private readonly IAppDbContext _context;
        private readonly IMapper _mapper;

        public SelectFullUserListHandler(IAppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SelectState<UserModel>> Handle(FullUserListQuery request, CancellationToken cancellationToken)
        {

            IEnumerable<UserModel> users = null;

            var data = _context.Users.Select(el => el).ProjectTo<UserModel>(_mapper.ConfigurationProvider);

            if (request.options != null)
            {
                users = await request.options.ApplyTo(data).Cast<UserModel>().ToListAsync(cancellationToken);
            }
            else {
                users = await _context.Users.Select(el => el).ProjectTo<UserModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
            }

           

            if (users == null)
            {
                return SelectState<UserModel>.Error(StatusCode.UserNotFound);
            }

            return SelectState<UserModel>.Success(users);
        }
    }
}
