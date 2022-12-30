using System.Diagnostics.CodeAnalysis;

namespace Blef.Shared.Abstractions.Commands;

[SuppressMessage(category: "ReSharper", checkId: "ClassNeverInstantiated.Global")]
[AttributeUsage(AttributeTargets.Class)]
public class CommandHandlerDecoratorAttribute : Attribute
{
}