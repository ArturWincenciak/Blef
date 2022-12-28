using System.Text.Json;

namespace Blef.Shared.Kernel.Exceptions;

public abstract class BlefException : Exception
{
    public string Title { get; }
    public string Detail { get; }
    public string Instance { get; }
    public IDictionary<string, string[]> Errors { get; }

    protected BlefException(string title, string detail, string instance)
        : base(JsonSerializer.Serialize(new {title, detail, instance}))
    {
        Title = title;
        Detail = detail;
        Instance = instance;
        Errors = new Dictionary<string, string[]>();
    }

    public BlefException WithError(ExceptionError error)
    {
        Errors.Add(error.Code, error.Values);
        return this;
    }
}