using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace Blef.Shared.Infrastructure.Tracing;

internal class TraceMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        context.Response.Headers.Add("traceId", context.TraceIdentifier);
        context.Response.Headers.Add("activityId", Activity.Current?.Id!);
        await next(context);
    }
}