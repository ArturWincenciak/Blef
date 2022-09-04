using System.Collections.Concurrent;
using System.Net;
using Blef.Shared.Kernel.Exceptions;
using Humanizer;
using Microsoft.AspNetCore.Mvc;

namespace Blef.Shared.Infrastructure.Exceptions;

internal class ExceptionToResponseMapper
{
    private static readonly ConcurrentDictionary<Type, string> CodesCache = new();

    public object Map(Exception exception) =>
        exception switch
        {
            BlefException ex => CreateBadRequest(ex),
            _ => CreateInternalServerError()
        };

    private static ProblemDetails CreateBadRequest(BlefException exception) =>
        exception.Errors.Any()
            ? CreateValidationProblemDetails(exception)
            : CreateProblemDetails(exception);

    private static ProblemDetails CreateProblemDetails(BlefException exception) =>
        new ()
        {
            Type = $"{DocumentationUrl}/{GetErrorCode(exception)}.md",
            Title = exception.Title,
            Status = (int) HttpStatusCode.BadRequest,
            Detail = exception.Detail,
            Instance = exception.Instance
        };

    private static ValidationProblemDetails CreateValidationProblemDetails(BlefException exception)
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
        {
            problemDetails.Errors.Add(error.Key, error.Value);
        }

        return problemDetails;
    }

    private static void WithTracing(ProblemDetails problemDetails)
    {
        problemDetails.Extensions["test1"] = "test1";
    }

    private static ProblemDetails CreateInternalServerError() =>
        new()
        {
            Type = $"{DocumentationUrl}/internal-server-error.md",
            Title = "Internal server error",
            Status = (int) HttpStatusCode.InternalServerError,
            Detail = "Unexpected error occurred"
        };

    private static string GetErrorCode(Exception exception)
    {
        var type = exception.GetType();

        if (CodesCache.TryGetValue(type, out var cachedErrorCode))
            return cachedErrorCode;

        var errorCode = CreateErrorCode(type);

        return CodesCache.GetOrAdd(type, errorCode);
    }

    private static string CreateErrorCode(Type type) =>
        type.Name.Underscore().Replace("_exception", string.Empty).Dasherize();

    private static string DocumentationUrl =>
        "https://github.com/ArturWincenciak/blef/doc/problem-details";
}