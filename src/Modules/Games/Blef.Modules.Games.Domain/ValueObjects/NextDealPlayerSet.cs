namespace Blef.Modules.Games.Domain.ValueObjects;

internal sealed class NextDealPlayerSet
{
    public IEnumerable<NextDealPlayer> Players { get; }

    public NextDealPlayerSet(IEnumerable<NextDealPlayer> players)
    {
        if (players is null)
            throw new ArgumentNullException(nameof(players));

        if (players.Count() < MIN_NUMBER_OF_PLAYERS)
            throw new ArgumentOutOfRangeException(nameof(players), "Next deal should have at least two players");

        if (players.Count() > MAX_NUMBER_OF_PLAYERS)
            throw new ArgumentOutOfRangeException(nameof(players), "Next deal cannot contain more than four players");

        if (AreAllPlayersUnique(players) == false)
            throw new ArgumentException("No player duplicates are allowed");

        Players = players;
    }


    private static bool AreAllPlayersUnique(IEnumerable<NextDealPlayer> NextDealPlayers) =>
        NextDealPlayers
            .Select(player => player.PlayerId)
            .Distinct()
            .Count() == NextDealPlayers.Count();
}