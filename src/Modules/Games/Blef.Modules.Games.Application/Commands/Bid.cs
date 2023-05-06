using Blef.Modules.Games.Domain.Model;
using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.Commands;

public sealed record Bid(GameId GameId, PlayerId PlayerId, string PokerHand) : ICommand;