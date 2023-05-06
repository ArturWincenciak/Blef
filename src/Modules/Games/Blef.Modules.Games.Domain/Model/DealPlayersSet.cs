﻿namespace Blef.Modules.Games.Domain.Model;

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

        var allDealtCards = players.SelectMany(player => player.Hand.Cards);
        if (AreAllCardsUnique(allDealtCards) == false)
            throw new ArgumentException("No card duplicates are allowed in the players' hands");

        Players = players;
    }

    public Table Table => new(Players.Select(player => player.Hand));

    private static bool AreAllPlayersUnique(IEnumerable<DealPlayer> players) =>
        players.Select(player => player.Player).Distinct().Count() == players.Count();

    private static bool AreAllCardsUnique(IEnumerable<Card> cards) =>
        cards.Distinct().Count() == cards.Count();
}