namespace Blef.Shared.Kernel.Exceptions;

public class NotFoundException : BlefException
{
    public NotFoundException(string detail, string instance)
        : base(title: "The resource with the given identifier does not exist", detail, instance)
    {
    }
}