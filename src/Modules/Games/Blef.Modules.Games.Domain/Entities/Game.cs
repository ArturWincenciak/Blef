using Blef.Modules.Games.Domain.Exceptions;
using Blef.Modules.Games.Domain.ValueObjects;

namespace Blef.Modules.Games.Domain.Entities;

public sealed class Game
{
    public GameId Id { get; }

    private const int MAX_NUMBER_OF_PLAYERS = 4;
    private const int MIN_NUMBER_OF_PLAYERS = 2;

    private readonly List<GamePlayer> _players = new();
    private readonly List<Deal> _deals = new();

    private Game(GameId id) =>
        Id = id;

    public static Game Create() =>
        new(id: new(Guid.NewGuid()));

    public GamePlayer Join(string nick)
    {
        if (IsGameStarted())
            throw new JoinGameThatIsAlreadyStartedException(Id, nick);

        if (_players.Count >= MAX_NUMBER_OF_PLAYERS)
            throw new MaxGamePlayersReachedException(Id);

        if (_players.Exists(player => player.Nick == nick))
            throw new PlayerAlreadyJoinedTheGameException(Id, nick);

        var player = GamePlayer.Create(nick);
        _players.Add(player);

        return player;
    }

    public DealId NewDeal()
    {
        // todo: check if there is not in progress deal
        // todo: check if is that player turn to deal
        // todo: check if in game is at least two players if it is first deal in the game
        // todo: check if game is not over
        // todo: if it is the first deal get all players joined game
        // todo: if it is next deal get only players that not loosed

        var number = _deals.Count + 1;
        var dealId = new DealId(Id, number);
        var players = _players.Select(p => p.Id);
        var deal = Deal.Create(dealId, players);
        _deals.Add(deal);
        return dealId;
    }

    private bool IsGameStarted() =>
        _deals.Count > 0;
}