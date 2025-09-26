using System.Net;

namespace API.Middleware;

public class ExceptionMiddleware(RequestDelegate next,
            ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{message}", ex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = env.IsDevelopment()
                ? new Error.ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString() ?? "")
                : new Error.ApiException(context.Response.StatusCode, "Internal Server Error", "");

            var options = new System.Text.Json.JsonSerializerOptions { PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase };
            var json = System.Text.Json.JsonSerializer.Serialize(response, options);
            await context.Response.WriteAsync(json);
        }
    }
}