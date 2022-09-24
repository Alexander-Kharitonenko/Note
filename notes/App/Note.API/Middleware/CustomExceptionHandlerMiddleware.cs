using FluentValidation;
using System.Net;
using System.Text.Json;

namespace Note.API.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next) 
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context) 
        {
            try 
            {
                await _next(context);
            }
            catch(Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
            
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception) 
        {
            var code = HttpStatusCode.InternalServerError;
            var resulr = string.Empty;

            switch (exception) 
            {
                case Exception exp:
                    code = HttpStatusCode.BadRequest;
                    resulr = JsonSerializer.Serialize(exp.Message);
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (string.IsNullOrEmpty(resulr)) 
            {
                resulr = JsonSerializer.Serialize(new { error = exception.Message });
            }

            return context.Response.WriteAsync(resulr);
        }
    }
}
