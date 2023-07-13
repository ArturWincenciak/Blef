namespace Blef.Shared.Kernel.Exceptions;

public abstract class NotFoundException : BlefException
{
    protected NotFoundException(string detail)
        : base(title: "The resource with the given identifier does not exist", detail)
    {
    }
}