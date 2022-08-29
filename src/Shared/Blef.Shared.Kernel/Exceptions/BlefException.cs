namespace Blef.Shared.Kernel.Exceptions;

//todo: is that good place?
public abstract class BlefException : Exception
{
    protected BlefException(string message)
        : base(message)
    {
    }
}