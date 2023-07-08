using Blef.Modules.Games.Domain.Events;
using Blef.Modules.Games.Domain.Exceptions;
using Blef.Modules.Games.Domain.Services;
using Blef.Shared.Abstractions.Events;

namespace Blef.Modules.Games.Domain.Model;

internal sealed class Game
{
    private readonly Croupier _croupier;

    private readonly List<Deal> _deals = new();
    private readonly List<GamePlayer> _players = new();

    public GameId Id { get; }

    public Game(GameId id, Croupier croupier)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        _croupier = croupier ?? throw new ArgumentNullException(nameof(croupier));
    }

    public GamePlayerJoined Join(PlayerNick nick)
    {
        if (IsGameStarted())
            throw new JoinGameAlreadyStartedException(Id, nick);

        if (_players.Count >= MAX_NUMBER_OF_PLAYERS)
            throw new TooManyPlayersException(Id);

        if (_players.Exists(player => player.Nick == nick))
            throw new PlayerAlreadyJoinedException(Id, nick);

        var joiningSequence = Order.Create(_players.Count + 1);
        var player = GamePlayer.Create(nick, joiningSequence);
        _players.Add(player);

        return new GamePlayerJoined(Id, player);
    }

    public DealStarted StartFirstDeal()
    {
        if (IsGameStarted())
            throw new GameAlreadyStartedException(Id);

        if (_players.Count < MIN_NUMBER_OF_PLAYERS)
            throw new NotEnoughPlayersException(Id);

        return NewDeal();
    }

    public BidPlaced Bid(Bid newBid)
    {
        Validate(newBid.Player);

        var lastDeal = _deals[^1];
        lastDeal.Bid(newBid);

        return new BidPlaced(Id, lastDeal.Id.Deal, newBid.Player, newBid.PokerHand);
    }

    public IReadOnlyCollection<IDomainEvent> Check(CheckingPlayer checkingPlayer)
    {
        Validate(checkingPlayer.Player);

        var lastDeal = _deals[^1];
        var dealLooser = lastDeal.Check(checkingPlayer);

        _players
            .Single(gamePlayer => gamePlayer.Id == dealLooser.Player)
            .LostLastDeal();

        var checkPlaced = new CheckPlaced(Id, lastDeal.Id.Deal, checkingPlayer, dealLooser);

        if (IsOnlyOnePlayerLeft())
        {
            var winner = _players.Single(player => player.IsInTheGame);
            var gameOver = new GameOver(Id, winner);
            return new IDomainEvent[] {checkPlaced, gameOver};
        }

        var nextDealStarted = NewDeal();
        return new IDomainEvent[] {checkPlaced, nextDealStarted};
    }

    private DealStarted NewDeal()
    {
        var nextDealNumber = _deals.Count + 1;
        var nextDealId = new DealId(Id, Deal: new DealNumber(nextDealNumber));
        var nextDealPlayers = CreateNextDealPlayers();
        var nextDealSet = _croupier.Deal(nextDealPlayers);

        _deals.Add(new Deal(nextDealId, nextDealSet));

        return new DealStarted(Id, nextDealId.Deal, nextDealSet.PlayersSet.Players);
    }

    private NextDealPlayersSet CreateNextDealPlayers()
    {
        var inGamePlayers = _players
            .Where(player => player.IsInTheGame)
            .ToArray();

        var playersCount = PlayersCount.Create(inGamePlayers.Length);
        var dealsPlayedCount = DealsCount.Create(_deals.Count);
        var orderPhysic = DealOrderPhysic.Create(playersCount, dealsPlayedCount);

        var nextDealPlayers = inGamePlayers
            .OrderBy(inGamePlayer => inGamePlayer.JoiningSequence)
            .Select((inGamePlayer, index) =>
            {
                var sequenceIndex = Order.Create(index + 1);
                var nextOrder = orderPhysic.ShiftedOrder(sequenceIndex);
                return new NextDealPlayer(inGamePlayer.Id, inGamePlayer.CardsAmount, nextOrder);
            })
            .ToArray();

        return new NextDealPlayersSet(nextDealPlayers);
    }

    private void Validate(PlayerId playerId)
    {
        ValidateGameInProgress();
        ValidatePlayer(playerId);
    }

    private void ValidateGameInProgress()
    {
        if (IsGameStarted() == false)
            throw new GameNotStartedException(Id);

        if (IsGameOver())
            throw new GameOverException(Id);
    }

    private bool IsGameStarted() =>
        _deals.Count > 0;

    private bool IsGameOver() =>
        IsOnlyOnePlayerLeft();

    private bool IsOnlyOnePlayerLeft() =>
        _players.Count(player => player.IsInTheGame) == 1;

    private void ValidatePlayer(PlayerId playerId)
    {
        ValidatePlayerJoinedToTheGame(playerId);
        ValidatePlayerStillInTheGame(playerId);
    }

    private void ValidatePlayerJoinedToTheGame(PlayerId playerId)
    {
        if (_players.Exists(player => player.Id == playerId) == false)
            throw new PlayerNotJoinedTheGameException(Id, playerId);
    }

    private void ValidatePlayerStillInTheGame(PlayerId playerId)
    {
        if (_players.Single(player => player.Id == playerId).IsInTheGame == false)
            throw new PlayerAlreadyLostTheGameException(Id, playerId);
    }
}