using System.Text.Json;

namespace Blef.Shared.Kernel.Exceptions;

public class SimpleBlefException : BlefException
{
    public SimpleBlefException(string message) 
        : base(message, message, null)
    {
    }
}