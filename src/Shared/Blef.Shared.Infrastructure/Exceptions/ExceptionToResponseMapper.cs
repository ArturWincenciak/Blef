using System.Collections.Concurrent;
using System.Net;
using System.Reflection;
using Blef.Shared.Kernel.Exceptions;
using Humanizer;
using Microsoft.AspNetCore.Mvc;

namespace Blef.Shared.Infrastructure.Exceptions;

internal sealed class ExceptionToResponseMapper
{
    private readonly ConcurrentDictionary<Type, string> _codesCache = new();

    private static string GitHubUrl => "https://github.com";
    private static string BlefUrl => $"{GitHubUrl}/ArturWincenciak/Blef";
    private static string DocumentationUrl => $"{BlefUrl}/blob/main/doc/problem-details";

    public object Map(Exception exception) =>
        exception switch
        {
            NotFoundException ex => CreateProblemDetails(ex, HttpStatusCode.NotFound),
            BlefException ex => CreateBadRequest(ex),
            _ => CreateInternalServerError()
        };

    private ProblemDetails CreateBadRequest(BlefException exception) =>
        CreateProblemDetails(exception, HttpStatusCode.BadRequest);

    private ProblemDetails CreateProblemDetails(BlefException exception, HttpStatusCode httpStatusCode) =>
        new()
        {
            Type = $"{DocumentationUrl}/{GetErrorCode(exception)}.md",
            Title = exception.Title,
            Status = (int) httpStatusCode,
            Detail = exception.Detail,
            Instance = exception.Instance
        };

    private static ProblemDetails CreateInternalServerError() =>
        new()
        {
            Type = $"{DocumentationUrl}/internal-server-error.md",
            Title = "Internal server error",
            Status = (int) HttpStatusCode.InternalServerError,
            Detail = "Unexpected error occurred"
        };

    private string GetErrorCode(Exception exception)
    {
        var type = exception.GetType();

        if (_codesCache.TryGetValue(type, value: out var cachedErrorCode))
            return cachedErrorCode;

        var errorCode = CreateErrorCode(type);

        return _codesCache.GetOrAdd(type, errorCode);
    }

    private static string CreateErrorCode(MemberInfo type) =>
        type.Name.Underscore().Replace(oldValue: "_exception", string.Empty).Dasherize();
}