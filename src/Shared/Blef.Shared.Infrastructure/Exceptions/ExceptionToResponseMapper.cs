using System.Collections.Concurrent;
using System.Net;
using Blef.Shared.Abstractions.Exceptions;
using Blef.Shared.Kernel.Exceptions;
using Humanizer;

namespace Blef.Shared.Infrastructure.Exceptions;

internal class ExceptionToResponseMapper : IExceptionToResponseMapper
{
    private static readonly ConcurrentDictionary<Type, string> CodesCache = new();

    public ExceptionResponse Map(Exception exception) =>
        exception switch
        {
            BlefException blefException => new ExceptionResponse(
                Response: CreateBadRequest(blefException),
                StatusCode: HttpStatusCode.BadRequest),
            _ => new ExceptionResponse(
                Response: CreateInternalServerError(),
                StatusCode: HttpStatusCode.InternalServerError)
        };

    private static BlefProblemDetails CreateBadRequest(BlefException exception) =>
        new BlefProblemDetails
            {
                Type = $"{DocumentationUrl}/{GetErrorCode(exception)}.md",
                Title = exception.Title,
                Status = (int) HttpStatusCode.BadRequest,
                Detail = exception.Detail,
                Instance = exception.Instance
            }
            .WithErrors(exception.Errors);

    private static BlefProblemDetails CreateInternalServerError() =>
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