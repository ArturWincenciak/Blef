namespace Blef.Shared.Abstractions.Exceptions;

public interface IExceptionToResponseMapper
{
    object Map(Exception ex);
}