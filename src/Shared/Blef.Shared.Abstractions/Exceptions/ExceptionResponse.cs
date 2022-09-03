using System.Net;

namespace Blef.Shared.Abstractions.Exceptions;

public record ExceptionResponse(BlefProblemDetails Response, HttpStatusCode StatusCode);