using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Blef.Shared.Kernel.Exceptions;

// todo: define exception for domain layer without any problem details structure
// domain should not know about problem details
// because domain layer don't know about API layer
// domain don't know about the HTTP on API layer
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
}