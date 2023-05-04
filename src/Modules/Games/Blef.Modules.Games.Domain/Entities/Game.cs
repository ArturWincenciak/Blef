using Blef.Modules.Games.Domain.Events;
using Blef.Modules.Games.Domain.Exceptions;
using Blef.Modules.Games.Domain.Services;
using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Cards;
using Blef.Modules.Games.Domain.ValueObjects.Ids;
using Blef.Shared.Abstractions.Events;

namespace Blef.Modules.Games.Domain.Entities;

internal sealed class Game
{
    private readonly Croupier _croupier;

    private readonly List<Deal> _deals = new();
    private readonly List<GamePlayer> _players = new();
    private readonly GamePlayer _lastStartingPlayer = null;

    public GameId GameId { get; }

    public Game(GameId id, Croupier croupier)
    {
        GameId = id ?? throw new ArgumentNullException(nameof(id));
        _croupier = croupier ?? throw new ArgumentNullException(nameof(croupier));
    }

    public GamePlayerJoined Join(PlayerNick nick)
    {
        if (IsGameStarted())
            throw new JoinGameAlreadyStartedException(GameId, nick);

        if (_players.Count >= MAX_NUMBER_OF_PLAYERS)
            throw new TooManyPlayersException(GameId);

        if (_players.Exists(player => player.Nick == nick))
            throw new PlayerAlreadyJoinedException(GameId, nick);

        var joiningSequence = _players.Count + 1;
        var player = GamePlayer.Create(nick, joiningSequence);
        _players.Add(player);

        return new GamePlayerJoined(GameId.Id, player.PlayerId.Id, player.Nick.Nick);
    }

    public DealStarted StartFirstDeal()
    {
        if (IsGameStarted())
            throw new GameAlreadyStartedException();

        if (_players.Count < MIN_NUMBER_OF_PLAYERS)
            throw new NotEnoughPlayersException(GameId);

        return NewDeal();
    }

    public BidPlaced Bid(DealId dealId, Bid newBid)
    {
        ValidateGameInProgress();

        var deal = GetDeal(dealId);
        deal.Bid(newBid);
        return new (GameId.Id, dealId.Number.Number, newBid.Player.Id, newBid.PokerHand.Serialize());
    }

    public IEnumerable<IDomainEvent> Check(DealId dealId, PlayerId checkingPlayerId)
    {
        ValidateGameInProgress();

        var deal = GetDeal(dealId);
        var looserPlayer = deal.Check(checkingPlayerId);
        var gamePlayer = _players.Single(gamePlayer => gamePlayer.PlayerId == looserPlayer.Player);
        gamePlayer.LostLastDeal();

        var nextDealStarted = NewDeal();

        var events = new IDomainEvent[]
        {
            new CheckPlaced(dealId.GameId.Id, dealId.Number.Number, checkingPlayerId.Id, looserPlayer.Player.Id),
            nextDealStarted
        };

        return events;
    }

    public Hand GetHand(PlayerId playerId, DealId dealId)
    {
        // todo: consider moving this to gameplay projection
        // todo: check if deal number exits
        // todo: check if user in the deal exists

        var deal = GetDeal(dealId);
        return deal.GetHand(playerId);
    }

    private DealStarted NewDeal()
    {
        var nextDealNumber = _deals.Count + 1;
        var nextDealId = new DealId(GameId, Number: new DealNumber(nextDealNumber));
        var nextDealPlayers = CreateNextDealPlayers();
        var nextDealSet = _croupier.Deal(nextDealPlayers);

        _deals.Add(new(nextDealId, nextDealSet));

        var dealStartedPlayers = Map(nextDealSet.PlayersSet);
        return new (GameId.Id, nextDealId.Number.Number, dealStartedPlayers);
    }

    private Deal GetDeal(DealId dealId) =>
        _deals.Single(d => d.DealId == dealId);

    private NextDealPlayersSet CreateNextDealPlayers() =>
        new (_players
            .Where(player => player.IsInTheGame)
            .Select(player => new NextDealPlayer(player.PlayerId, player.CardsAmount, player.JoiningSequence))
            .ToArray());

    private int CalculateNextDealOrder(GamePlayer player)
    {
        if (_lastStartingPlayer is null)
            return player.JoiningSequence;

        return player.JoiningSequence;

        /* todo:
        var playerJoiningNumber = player.JoiningSequence;
        var playerCount = _players.Count;
        var lastStartingNumber = _lastStartingPlayer.JoiningSequence;

        // first deal, last: null
        // p1 -> j = 1, order = 1
        // p2 -> j = 2, order = 2
        // p3 -> j = 3, order = 3
        // p4 -> j = 4, order = 4

        // second deal, last: p1
        // p1 -> j = 1, order = 4
        // p2 -> j = 2, order = 1
        // p3 -> j = 3, order = 2
        // p4 -> j = 4, order = 3

        // third deal, last: p2
        // p1 -> j = 1, order = 3
        // p2 -> j = 2, order = 4
        // p3 -> j = 3, order = 1
        // p4 -> j = 4, order = 2

        // fourth deal, last: p3
        // p1 -> j = 1, order = 2
        // p2 -> j = 2, order = 3
        // p3 -> j = 3, order = 4
        // p4 -> j = 4, order = 1

        // fifth deal, last: p4
        // p1 -> j = 1, order = 1
        // p2 -> j = 2, order = 2
        // p3 -> j = 3, order = 3
        // p4 -> j = 4, order = 4

        */
    }

    private static IEnumerable<DealStarted.Player> Map(DealPlayersSet dealPlayers)
    {
        var advancingPlayers = dealPlayers.Players.Select(player =>
            new DealStarted.Player(player.Player.Id, player.Hand.Cards.Select(card =>
                new DealStarted.Card(card.FaceCard.ToString(), card.Suit.ToString()))));
        return advancingPlayers;
    }

    private bool IsGameStarted() =>
        _deals.Count > 0;

    private void ValidateGameInProgress()
    {
        if (IsGameStarted() == false)
            throw new GameNotStartedException(GameId);

        if (IsGameOver())
            throw new GameOverException(GameId);
    }

    private bool IsGameOver()
    {
        // todo: implement
        return false;
    }
}