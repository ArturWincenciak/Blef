using Blef.Shared.Abstractions.Commands;
using System.ComponentModel.DataAnnotations;

namespace Blef.Modules.Games.Application.ErrorHandlingPlayground.Commands;

public sealed record JustCommand(
    [Required] [MinLength(3)] [MaxLength(5)] string Stuff,
    [Required] [Range(3, 6)] int Count) : ICommand;