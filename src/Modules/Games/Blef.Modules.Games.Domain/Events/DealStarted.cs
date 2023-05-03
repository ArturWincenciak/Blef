using Blef.Shared.Abstractions.Events;

namespace Blef.Modules.Games.Domain.Events;

internal sealed record DealStarted(
        Guid GameId,
        int DealNumber,
        IEnumerable<DealStarted.Player> Players)
    : IDomainEvent<DealStarted>
{
    internal sealed record Player(Guid PlayerId, IEnumerable<Card> Hand);

    internal sealed record Card(string FaceCard, string Suit);
}