using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WebApi.Middleware;

public class ErrorHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = new ProblemDetails
        {
            Title = "An unexpected error ocurred!",
            Detail = exception.Message,
            Status = StatusCodes.Status500InternalServerError,
            Instance = context.Request.Path
        };
        response.Extensions.Add("traceId", context.TraceIdentifier);
        response.Extensions.Add("exceptionType", exception.GetType().Name);

        var json = JsonSerializer.Serialize(response);
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = response.Status.Value;
        return context.Response.WriteAsync(json);
    }
}

public static class ErrorHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlingMiddleware>();
    }
}
