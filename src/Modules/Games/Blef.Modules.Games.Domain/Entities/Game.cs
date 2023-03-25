using Blef.Modules.Games.Domain.Exceptions;
using Blef.Modules.Games.Domain.ValueObjects;

namespace Blef.Modules.Games.Domain.Entities;

public sealed class Game
{
    private const int MAX_NUMBER_OF_PLAYERS = 4;
    private const int MIN_NUMBER_OF_PLAYERS = 2;

    private bool _isGameStarted;

    private readonly List<GamePlayer> _players = new();
    private readonly List<Deal> _deals = new(); // todo

    public Guid Id { get; }

    public Game(Guid id) =>
        Id = id;

    public static Game Create() =>
        new(id: Guid.NewGuid());

    public GamePlayer Join(string nick)
    {
        if (_isGameStarted)
            throw new JoinGameThatIsAlreadyStartedException(Id, nick);

        if (_players.Count >= MAX_NUMBER_OF_PLAYERS)
            throw new MaxGamePlayersReachedException(Id);

        if (_players.Exists(player => player.Nick == nick))
            throw new PlayerAlreadyJoinedTheGameException(Id, nick);

        var player = new GamePlayer();
        _players.Add(player);

        return player;
    }

    public (
        IReadOnlyCollection<(Guid PlayerId, string Nick, Card[] Cards)> Players,
        IReadOnlyCollection<(int Order, Guid PlayerId, string Bid)> Bids,
        Guid CheckingPlayerId,
        Guid LooserPlayerId
        ) GetFlow()
    {
        // todo: get deals and forward the query to each deal
        throw new NotImplementedException();
    }
}