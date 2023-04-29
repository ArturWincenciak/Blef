using Blef.Modules.Games.Domain.ValueObjects;

namespace Blef.Modules.Games.Domain.Entities;

internal sealed class DealPlayersSet
{
    public IEnumerable<DealPlayer> Players { get; }

    public DealPlayersSet(IEnumerable<DealPlayer> players)
    {
        if (players is null)
            throw new ArgumentNullException(nameof(players));

        if (players.Count() < MIN_NUMBER_OF_PLAYERS)
            throw new ArgumentOutOfRangeException(nameof(players), "Deal should have at least two players");

        if (players.Count() > MAX_NUMBER_OF_PLAYERS)
            throw new ArgumentOutOfRangeException(nameof(players), "Deal cannot have more than four players");

        if (AreAllPlayersUnique(players) == false)
            throw new ArgumentException("No player duplicates are allowed");

        Players = players;
    }

    private bool AreAllPlayersUnique(IEnumerable<DealPlayer> players) =>
        players
            .Select(player => player.PlayerId)
            .Distinct()
            .Count() ==
        players.Count();
}