using System.Net;
using System.Text.Json;
using FluentValidation;
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
                UnauthorizedException e => (HttpStatusCode.Unauthorized, e.Message),
                NotFoundException e => (HttpStatusCode.NotFound, e.Message),
                ConflictException e => (HttpStatusCode.Conflict, e.Message),
                ForbiddenException e => (HttpStatusCode.Forbidden, e.Message),
                ValidationException e => (HttpStatusCode.UnprocessableEntity, string.Join("; ", e.Errors.Select( err => err.ErrorMessage))),
                _ => (HttpStatusCode.InternalServerError, "An unexpected error occured"),
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                Success = false,
                Message = message,
                Errors = ex is ValidationException ve ? [.. ve.Errors.Select(e => e.ErrorMessage)] : new List<string> {message}
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
