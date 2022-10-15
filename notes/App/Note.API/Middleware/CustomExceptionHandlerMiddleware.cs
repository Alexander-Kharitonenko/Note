using FluentValidation;
using Fluid.Parser;
using Notes.Environment.Base;
using Notes.Environment.SystemConstants;
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
            var errorMessage = string.Empty;
            var error = string.Empty;

            switch (exception) 
            {
                case Exception exp:
                    errorMessage = JsonSerializer.Serialize(exp.Message);
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (string.IsNullOrEmpty(error)) 
            {
               error = JsonSerializer.Serialize(new { error = errorMessage, StatusCode = code });
            }

            return context.Response.WriteAsync(error);
        }
    }
}
