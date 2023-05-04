using Blef.Modules.Games.Domain.ValueObjects.Ids;
using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.Commands;

public sealed record Bid(GameId GameId, PlayerId PlayerId, string PokerHand) : ICommand;