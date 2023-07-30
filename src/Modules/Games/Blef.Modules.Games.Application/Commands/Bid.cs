using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.Commands;

// todo: use everywhere the domain objects instead of primitives
// here in only one example place, but it should be everywhere
public sealed record Bid(Guid GameId, Guid PlayerId, string PokerHand) : ICommand<Bid.Result>
{
    public sealed record Result(int DealNumber) : ICommandResult;
}