namespace Blef.Modules.Games.Domain.Model;

internal sealed class NextDealPlayersSet
{
    public IReadOnlyCollection<NextDealPlayer> Players { get; }

    public NextDealPlayersSet(IReadOnlyCollection<NextDealPlayer> players)
    {
        if (players is null)
            throw new ArgumentNullException(nameof(players));

        if (players.Count < MIN_NUMBER_OF_PLAYERS)
            throw new ArgumentOutOfRangeException(paramName: nameof(players), players.Count,
                message: $"Next deal should have at least {MIN_NUMBER_OF_PLAYERS} players");

        if (players.Count > MAX_NUMBER_OF_PLAYERS)
            throw new ArgumentOutOfRangeException(paramName: nameof(players), players.Count,
                message: $"Next deal cannot contain more than {MAX_NUMBER_OF_PLAYERS} players");

        if (AreAllPlayersUnique(players) == false)
            throw new ArgumentException("No player duplicates are allowed");

        Players = players;
    }

    private static bool AreAllPlayersUnique(IReadOnlyCollection<NextDealPlayer> nextDealPlayers) =>
        nextDealPlayers
            .Select(nextDealPlayer => nextDealPlayer.Player)
            .Distinct()
            .Count() ==
        nextDealPlayers.Count;
}