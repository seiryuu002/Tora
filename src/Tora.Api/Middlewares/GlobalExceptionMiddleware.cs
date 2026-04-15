using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using Tora.Domain.Exceptions;

namespace Tora.Api.Middlewares;

public class GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch(Exception ex)
        {
            logger.LogError(ex, "An unhandled exception for {Method} {Path}", context.Request.Method, context.Request.Path);

            var (statusCode, message) = ex switch
            {
                NotFoundException e => (HttpStatusCode.NotFound, e.Message),
                ConflictException e => (HttpStatusCode.Conflict, e.Message),
                ForbiddenException e => (HttpStatusCode.Forbidden, e.Message),
                ValidationException e => (HttpStatusCode.UnprocessableEntity, string.Join("; ", e.Message)),
                _ => (HttpStatusCode.InternalServerError, "An unexpected error occured"),
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                Success = false,
                Message = "Something went wrong while processing your request",
                Errors = ex is ValidationException ve ? new List<string> {ve.Message} : [message]
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
    

}
