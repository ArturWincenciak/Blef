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
                Response: new ErrorResponse(
                    Errors: new Error(
                        Code: GetErrorCode(blefException),
                        Message: exception.Message)),
                StatusCode: HttpStatusCode.BadRequest),
            _ => new ExceptionResponse(
                Response: new ErrorResponse(
                    Errors: new Error(
                        Code: "error",
                        Message: "There was an error.")),
                StatusCode: HttpStatusCode.InternalServerError)
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
        type.Name.Underscore().Replace("_exception", string.Empty);

    private record Error(string Code, string Message);

    private record ErrorResponse(params Error[] Errors);
}