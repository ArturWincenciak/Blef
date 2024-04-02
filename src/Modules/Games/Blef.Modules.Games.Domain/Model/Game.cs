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

        return new(Id, player);
    }

    public DealStarted StartFirstDeal()
    {
        if (IsGameStarted())
            throw new GameAlreadyStartedException(Id);

        if (_players.Count < MIN_NUMBER_OF_PLAYERS)
            throw new NotEnoughPlayersException();

        return NewDeal(FirstDealPlayers);
    }

    public BidPlaced Bid(Bid newBid)
    {
        Validate(newBid.Player);

        var lastDeal = _deals[^1];
        lastDeal.Bid(newBid);

        return new(Id, lastDeal.Id.Deal, newBid.Player, newBid.PokerHand);
    }

    public IReadOnlyCollection<IDomainEvent> Check(CheckingPlayer checkingPlayer)
    {
        Validate(checkingPlayer.Player);

        var lastDeal = _deals[^1];
        var lastDealLooser = lastDeal.Check(checkingPlayer);

        _players
            .Single(gamePlayer => gamePlayer.Id == lastDealLooser.Player)
            .LostLastDeal();

        var checkPlaced = new CheckPlaced(Id, lastDeal.Id.Deal, checkingPlayer, lastDealLooser);

        if (IsOnlyOnePlayerLeft())
        {
            var winner = _players.Single(player => player.IsInTheGame);
            var gameOver = new GameOver(Id, winner);
            return new IDomainEvent[] {checkPlaced, gameOver};
        }

        var nextDealStarted = NewDeal(() => NextDealPlayers(lastDealLooser));
        return new IDomainEvent[] {checkPlaced, nextDealStarted};
    }

    private DealStarted NewDeal(Func<NextDealPlayersSet> dealPlayers)
    {
        var nextDealNumber = _deals.Count + 1;
        var nextDealId = new DealId(Id, Deal: new(nextDealNumber));
        var nextDealPlayers = dealPlayers();
        var nextDealSet = _croupier.Deal(nextDealPlayers);

        _deals.Add(new(nextDealId, nextDealSet));

        return new(Id, nextDealId.Deal, nextDealSet.PlayersSet.Players);
    }

    private NextDealPlayersSet FirstDealPlayers() =>
        new(_players
            .OrderBy(inGamePlayer => inGamePlayer.JoiningSequence)
            .Select(inGamePlayer =>
                new NextDealPlayer(
                    inGamePlayer.Id,
                    inGamePlayer.CardsAmount,
                    inGamePlayer.JoiningSequence))
            .ToArray());

    private NextDealPlayersSet NextDealPlayers(LooserPlayer lastDealLooser)
    {
        var lastLooserJoiningSequence = _players
            .Single(gamePlayer => gamePlayer.Id == lastDealLooser.Player)
            .JoiningSequence.Int;

        var nextDealPlayers = BuildNextDealPlayers(nextIndex: lastLooserJoiningSequence + 1);
        return new(nextDealPlayers.OrderBy(dealPlayer => dealPlayer.Order).ToArray());
    }

    private IEnumerable<NextDealPlayer> BuildNextDealPlayers(int nextIndex, List<NextDealPlayer>? result = null,
        int index = 1)
    {
        while (true)
        {
            result ??= new();

            var (nextGamePlayer, nextOrder) = FindNextInGamePlayer(nextIndex);

            if (result.Exists(item => item.Player == nextGamePlayer.Id) == false)
            {
                result.Add(new(
                    nextGamePlayer.Id, nextGamePlayer.CardsAmount, Order: Order.Create(index)));

                nextIndex = nextOrder + 1;
                index += 1;

                continue;
            }

            return result;
        }
    }

    private (GamePlayer Player, int Order) FindNextInGamePlayer(int order)
    {
        if (order > _players.Count)
            order = 1;

        var firstPlayer = _players.Single(player => player.JoiningSequence.Int == order);
        return firstPlayer.IsInTheGame
            ? (firstPlayer, order)
            : FindNextInGamePlayer(order + 1);
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
            throw new GameOverException();
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