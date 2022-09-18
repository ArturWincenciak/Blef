using System.ComponentModel.DataAnnotations;
using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.Playground.Commands;

public sealed record JustCommand(
    [Required] [MinLength(3)] [MaxLength(5)] string Stuff,
    [Required] [Range(3, 6)] int Count) : ICommand;