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

    private static string DocumentationUrl =>
        "https://github.com/ArturWincenciak/Blef/blob/main/doc/problem-details";

    public object Map(Exception exception) =>
        exception switch
        {
            BlefException ex => CreateBadRequest(ex),
            _ => CreateInternalServerError()
        };

    private ProblemDetails CreateBadRequest(BlefException exception) =>
        exception.Errors.Any()
            ? CreateValidationProblemDetails(exception)
            : CreateProblemDetails(exception);

    private ProblemDetails CreateProblemDetails(BlefException exception) =>
        new()
        {
            Type = $"{DocumentationUrl}/{GetErrorCode(exception)}.md",
            Title = exception.Title,
            Status = (int) HttpStatusCode.BadRequest,
            Detail = exception.Detail,
            Instance = exception.Instance
        };

    private ValidationProblemDetails CreateValidationProblemDetails(BlefException exception)
    {
        var problemDetails = new ValidationProblemDetails
        {
            Type = $"{DocumentationUrl}/{GetErrorCode(exception)}.md",
            Title = exception.Title,
            Status = (int) HttpStatusCode.BadRequest,
            Detail = exception.Detail,
            Instance = exception.Instance
        };

        foreach (var error in exception.Errors)
            problemDetails.Errors.Add(error.Key, error.Value);

        return problemDetails;
    }

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