using System.Diagnostics;
using Blef.Shared.Abstractions.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace Blef.Shared.Infrastructure.Exceptions;

internal class ErrorHandlerMiddleware : IMiddleware
{
    private readonly IExceptionToResponseMapper _exceptionMapper;

    public ErrorHandlerMiddleware(IExceptionToResponseMapper exceptionMapper) =>
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
        errorResponse.Response.WithTraceId(context.TraceIdentifier);
        errorResponse.Response.WithActivityId(Activity.Current?.Id!);
        context.Response.StatusCode = (int) errorResponse.StatusCode;
        var response = errorResponse.Response;
        await context.Response.WriteAsJsonAsync(response);
    }
}