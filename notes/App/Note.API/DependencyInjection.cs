using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Commands.Notes.Create;
using Commands.Notes.Delete;
using Commands.Notes.Update;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Notes.Domain.Interfaces;
using Notes.Persistence.MySQL;
using Queries.Notes;
using Commands.Users.Create;
using Queries.Users;
using Commands.Users.Update;
using Commands.Users.Delete;
using Microsoft.IdentityModel.Tokens;
using Note.API.Services.Authentication;
using Microsoft.Extensions.Options;
using Note.API.Services.Authentication.Access;
using Note.API.Services.ViewMapper;
using Notes.Environment.Commands;
using Notes.Environment.Queries;

namespace Note.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionString"];

            services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
            services.AddScoped<IAppDbContext>(provider => provider.GetService<AppDbContext>() ?? new AppDbContext(new DbContextOptions<AppDbContext>()));

            return services;
        }

        public static IServiceCollection AddMediatr(this IServiceCollection services, params Assembly[] assembly)
        {
            services.AddScoped<IRequestHandler<CreateNoteCommand, CommandState>, CreateNoteHandler>();
            services.AddScoped<IRequestHandler<UpdateNoteCommand, CommandState>, UpdateNoteHandler>();
            services.AddScoped<IRequestHandler<DeleteNoteCommand, CommandState>, DeleteNoteHandler>();
            services.AddScoped<IRequestHandler<FindNoteQuery, QueryState<NoteModel>>, FindNoteHandler>();
            services.AddScoped<IRequestHandler<FullNoteListQuery, SelectState<NoteModel>>, SelectFullNoteListHandler>();
            services.AddScoped<IRequestHandler<CreateUserCommand, CommandState>, CreateUserHandler>();
            services.AddScoped<IRequestHandler<UpdateUserCommand, CommandState>, UpdateUserHandler>();
            services.AddScoped<IRequestHandler<DeleteUserCommand, CommandState>, DeleteUserHandler>();
            services.AddScoped<IRequestHandler<FindUserQuery, QueryState<UserModel>>, FindUserHandler>();
            services.AddScoped<IRequestHandler<FullUserListQuery, SelectState<UserModel>>, SelectFullUserListHandler>();
            services.AddMediatR(assembly);
            return services;
        }

        public static IServiceCollection AddCastomServices(this IServiceCollection services) 
        {
            services.AddScoped<IAuthenticationManaget, AuthenticationManaget>();
            services.AddScoped<IViewMapperService, ViewMapperService>();
            return services;
        }

        public static IServiceCollection AddFluentValidator(this IServiceCollection services, IEnumerable<Assembly> assembly)
        {
            services.AddValidatorsFromAssemblies(assembly);
            return services;
        }

        public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var _authOptions = configuration.GetSection("JWT")
                                                      .Get<AuthOptions>();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.RequireAuthenticatedSignIn = true;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.SaveToken = true;
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = _authOptions.Issuer,
                    ValidateIssuer = true,
                    ValidAudience = _authOptions.Audience,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.Key)),
                };
            });

            return services;
        } 
    }
}
