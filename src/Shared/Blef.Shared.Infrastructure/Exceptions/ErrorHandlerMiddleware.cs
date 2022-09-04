using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blef.Shared.Infrastructure.Exceptions;

internal class ErrorHandlerMiddleware : IMiddleware
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
        catch (Exception ex)
        {
            await HandleErrorAsync(context, ex);
        }
    }

    private async Task HandleErrorAsync(HttpContext context, Exception ex)
    {
        var errorResponse = _exceptionMapper.Map(ex);
        ((ProblemDetails) errorResponse).Extensions["traceId"] = context.TraceIdentifier;
        ((ProblemDetails) errorResponse).Extensions["activityId"] = Activity.Current?.Id!;
        context.Response.ContentType = "application/problem+json; charset=utf-8"; //todo:...
        context.Response.StatusCode = (int) ((ProblemDetails) errorResponse).Status!;
        await context.Response.WriteAsJsonAsync(errorResponse);
    }
}
