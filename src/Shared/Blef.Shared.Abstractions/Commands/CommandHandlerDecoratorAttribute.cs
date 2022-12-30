using System.Diagnostics.CodeAnalysis;

namespace Blef.Shared.Abstractions.Commands;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
[AttributeUsage(AttributeTargets.Class)]
public class CommandHandlerDecoratorAttribute : Attribute
{
}