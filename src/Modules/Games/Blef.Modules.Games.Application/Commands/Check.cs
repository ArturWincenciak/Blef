using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.Commands;

public sealed record Check(GameId GameId, PlayerId PlayerId) : ICommand;