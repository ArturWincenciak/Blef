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
        bool isGameStarted = _deals.Count > 0;
        if (isGameStarted)
            throw new JoinGameThatIsAlreadyStartedException(Id, nick);

        if (_players.Count >= MAX_NUMBER_OF_PLAYERS)
            throw new MaxGamePlayersReachedException(Id);

        if (_players.Exists(player => player.Nick == nick))
            throw new PlayerAlreadyJoinedTheGameException(Id, nick);

        var player = GamePlayer.Create(nick);
        _players.Add(player);

        return player;
    }
}