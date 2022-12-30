using System.Diagnostics.CodeAnalysis;

namespace Blef.Shared.Kernel.Exceptions;

[SuppressMessage(category: "ReSharper", checkId: "ClassNeverInstantiated.Global")]
public record ExceptionError(string Code, string[] Values);