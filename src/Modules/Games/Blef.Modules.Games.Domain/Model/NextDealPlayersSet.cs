﻿namespace Blef.Modules.Games.Domain.Model;

internal sealed class NextDealPlayersSet
{
    public IEnumerable<NextDealPlayer> Players { get; }

    public NextDealPlayersSet(IEnumerable<NextDealPlayer> players)
    {
        if (players is null)
            throw new ArgumentNullException(nameof(players));

        if (players.Count() < MIN_NUMBER_OF_PLAYERS)
            throw new ArgumentOutOfRangeException(paramName: nameof(players), actualValue: players.Count(),
                message: $"Next deal should have at least {MIN_NUMBER_OF_PLAYERS} players");

        if (players.Count() > MAX_NUMBER_OF_PLAYERS)
            throw new ArgumentOutOfRangeException(paramName: nameof(players), actualValue: players.Count(),
                message: $"Next deal cannot contain more than {MAX_NUMBER_OF_PLAYERS} players");

        if (AreAllPlayersUnique(players) == false)
            throw new ArgumentException("No player duplicates are allowed");

        Players = players;
    }

    private static bool AreAllPlayersUnique(IEnumerable<NextDealPlayer> NextDealPlayers) =>
        NextDealPlayers
            .Select(nextDealPlayer => nextDealPlayer.Player)
            .Distinct()
            .Count() ==
        NextDealPlayers.Count();
}