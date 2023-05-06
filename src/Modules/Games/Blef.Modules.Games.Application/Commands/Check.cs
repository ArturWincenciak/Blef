using Blef.Modules.Games.Domain.Model;
using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.Commands;

public sealed record Check(GameId GameId, PlayerId PlayerId) : ICommand;