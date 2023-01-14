using Blef.Modules.Games.Domain.Exceptions;

namespace Blef.Modules.Games.Domain.Entities;

public class Tournament
{
    private readonly List<Game> _games = new();
    private readonly List<TournamentPlayer> _players = new();
    private bool _isTournamentStarted;

    public Guid Id { get; }

    private Tournament(Guid id) =>
        Id = id;

    public static Tournament Create() =>
        new(Guid.NewGuid());

    public TournamentPlayer Join(string nick)
    {
        if (_isTournamentStarted)
            throw new JoinTournamentThatIsAlreadyStartedException(Id, nick);

        if (_players.Count >= 2)
            throw new MaxTournamentPlayersReachedException(Id);

        if (_players.Exists(player => player.Nick == nick))
            throw new PlayerAlreadyJoinedTheTournamentException(Id, nick);

        var player = TournamentPlayer.Create(nick);
        _players.Add(player);

        return player;
    }

    public void Start()
    {
        if (_isTournamentStarted)
            throw new TournamentHasBeenAlreadyStartedException(Id);

        _isTournamentStarted = true;
    }

    public IEnumerable<TournamentPlayer> GetPlayers() =>
        _players;

    public void AddGame(Game game)
    {
        if (_isTournamentStarted == false)
            throw new InvalidOperationException("Cannot add games for not started tournament");

        _games.Add(game);
    }

    public Game GetCurrentGame() =>
        _games[^1];
}