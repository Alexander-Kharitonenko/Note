using Commands;
using Commands.Notes;
using MediatR;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Note.API.Middleware;
using Note.API.Services.Authentication.Access;
using Notes.Domain.Interfaces;
using Notes.Persistence.MySQL;
using NSwag;
using NSwag.Generation.Processors.Security;
using Queries;
using Queries.Notes;
using System.Net;
using System.Reflection;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.OData;
using NJsonSchema;
using Tools.Swagger;

namespace Note.API
{
    public class Program
    {
        public IConfiguration Configuration { get; }

        public Program(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<AuthOptions>(builder.Configuration.GetSection("JWT"));

            builder.Services.AddControllers().AddOData();

            builder.Services.AddOpenApiDocument(document =>
            {
                document.SchemaType = SchemaType.OpenApi3;

                document.AddSecurity("Bearer", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT",
                    Description = "Type into the textbox: {your JWT token}."
                });

                document.OperationProcessors.Add(
                    new AspNetCoreOperationSecurityScopeProcessor("Bearer"));
                document.OperationProcessors.Add(new ODataQueryOperationProcessor());        
            }
            );


            builder.Services.AddAutoMapper(new Assembly[] {
                Assembly.GetExecutingAssembly(),
                CommandsAssembly.Value,
                QueriesAssembly.Value,
                typeof(IAppDbContext).Assembly,
            });

            builder.Services.AddFluentValidator(new List<Assembly>()
            {
                Assembly.GetExecutingAssembly(),
                QueriesAssembly.Value,
                CommandsAssembly.Value
            });
            builder.Services.AddContext(builder.Configuration);
            builder.Services.AddMediatr(Assembly.GetExecutingAssembly());
            builder.Services.AddJwt(builder.Configuration);
            builder.Services.AddCastomServices();


            builder.Services.AddCors(options =>
                 options.AddPolicy("AllowAll",
                            opt =>
                            {
                                opt.WithOrigins("http://localhost:3200/", "https://top-notes.vercel.app/", "https://notereactfeddim72.netlify.app/");
                                opt.AllowAnyHeader();
                                opt.AllowAnyMethod();
                                opt.AllowAnyOrigin();
                            }));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseOpenApi();
                app.UseSwaggerUi3();
            }

            app.UseStaticFiles();
            app.UseCastomExceptionHandler();

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("AllowAll");

            app.UseEndpoints(options =>
            {
                options.MapControllers();
            });

            using (var scope = app.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                try
                {
                    var context = provider.GetRequiredService<AppDbContext>();
                    DbInitializer.Initializer(context);
                    DbInitializer.AddMigration(context);
                }
                catch (Exception e)
                {
                    //TODO: добавить логирование
                }
            }

            app.Run();
        }
    }
}