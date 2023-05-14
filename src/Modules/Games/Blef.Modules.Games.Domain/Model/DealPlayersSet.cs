namespace Blef.Modules.Games.Domain.Model;

internal sealed class DealPlayersSet
{
    public IEnumerable<DealPlayer> Players { get; }

    public Table Table => new(Players.Select(player => player.Hand));

    public DealPlayersSet(IEnumerable<DealPlayer> players)
    {
        if (players is null)
            throw new ArgumentNullException(nameof(players));

        switch (players.Count())
        {
            case < MIN_NUMBER_OF_PLAYERS:
                throw new ArgumentOutOfRangeException(paramName: nameof(players), actualValue: players.Count(),
                    message: $"Deal should have at least {MIN_NUMBER_OF_PLAYERS} players");
            case > MAX_NUMBER_OF_PLAYERS:
                throw new ArgumentOutOfRangeException(paramName: nameof(players), actualValue: players.Count(),
                    message: $"Deal cannot have more than {MAX_NUMBER_OF_PLAYERS} players");
        }

        if (AreAllPlayersUnique(players) == false)
            throw new ArgumentException("No player duplicates are allowed");

        var allDealtCards = players.SelectMany(player => player.Hand.Cards);
        if (AreAllCardsUnique(allDealtCards) == false)
            throw new ArgumentException("No card duplicates are allowed in the players' hands");

        Players = players;
    }

    private static bool AreAllPlayersUnique(IEnumerable<DealPlayer> players) =>
        players.Select(player => player.Player).Distinct().Count() == players.Count();

    private static bool AreAllCardsUnique(IEnumerable<Card> cards) =>
        cards.Distinct().Count() == cards.Count();
}