using System.Net;
using System.Text.Json;

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
            logger.LogError(ex, "An unhandled exception occured while processing the request");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                Success = false,
                Message = "Something went wrong while processing your request",
                Errors = new List<string> { ex.Message}
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
    

}
