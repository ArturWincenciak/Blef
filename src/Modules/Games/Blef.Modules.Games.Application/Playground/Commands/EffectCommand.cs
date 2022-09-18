using System.ComponentModel.DataAnnotations;
using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.Playground.Commands;

public sealed record EffectCommand(
    [Required] [Range(1, int.MaxValue)] int Amount,
    [Required] bool Flag) : ICommand<EffectCommand.Result>
{
    public sealed record Result(string[] Effect) : ICommandResult;
}
