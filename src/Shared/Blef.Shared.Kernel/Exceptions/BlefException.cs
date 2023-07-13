using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Blef.Shared.Kernel.Exceptions;

public abstract class BlefException : Exception
{
    public string Title { get; }
    public string Detail { get; }
    public string Instance { get; private set; }
    public IDictionary<string, string[]> Errors { get; }

    protected BlefException(string title, string detail)
        : base(JsonSerializer.Serialize(new {title, detail}))
    {
        Title = title;
        Detail = detail;
        Errors = new Dictionary<string, string[]>();
        Instance = string.Empty;
    }

    public BlefException WithInstance(string instance)
    {
        Instance = instance;
        return this;
    }

    [SuppressMessage(category: "ReSharper", checkId: "UnusedMember.Global")]
    public BlefException WithError(ExceptionError error)
    {
        Errors.Add(error.Code, error.Values);
        return this;
    }
}