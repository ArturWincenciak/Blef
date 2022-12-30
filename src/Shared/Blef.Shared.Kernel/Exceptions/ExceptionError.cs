using System.Diagnostics.CodeAnalysis;

namespace Blef.Shared.Kernel.Exceptions;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public record ExceptionError(string Code, string[] Values);