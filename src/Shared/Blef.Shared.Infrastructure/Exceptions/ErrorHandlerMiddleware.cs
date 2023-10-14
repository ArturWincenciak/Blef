using System.Diagnostics;
using Blef.Shared.Kernel.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blef.Shared.Infrastructure.Exceptions;

internal sealed class ErrorHandlerMiddleware : IMiddleware
{
    private readonly ExceptionToResponseMapper _exceptionMapper;

    public ErrorHandlerMiddleware(ExceptionToResponseMapper exceptionMapper) =>
        _exceptionMapper = exceptionMapper;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (BlefException ex)
        {
            ex = ex.WithInstance(context.Request.Path);
            await HandleErrorAsync(context, ex);
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(context, ex);
        }
    }

    private async Task HandleErrorAsync(HttpContext context, Exception ex)
    {
        SetProblemDetailsContentType(context);
        var errorResponse = _exceptionMapper.Map(ex);
        ((ProblemDetails) errorResponse).Extensions["traceId"] = context.TraceIdentifier;
        ((ProblemDetails) errorResponse).Extensions["activityId"] = Activity.Current?.Id!;
        context.Response.StatusCode = (int) ((ProblemDetails) errorResponse).Status!;
        await context.Response.WriteAsJsonAsync(errorResponse);
    }

    private static void SetProblemDetailsContentType(HttpContext context) =>
        context.Response.OnStarting(callback: state =>
        {
            var httpContext = (HttpContext) state;
            httpContext.Response.Headers.ContentType = "application/problem+json; charset=utf-8";
            return Task.CompletedTask;
        }, context);
}