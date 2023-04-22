using Blef.Modules.Games.Domain.Exceptions;
using Blef.Modules.Games.Domain.Services;
using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Cards;
using Blef.Modules.Games.Domain.ValueObjects.Dto;
using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.Entities;

internal sealed class Game
{
    private readonly Croupier _croupier;

    private const int MAX_NUMBER_OF_PLAYERS = 4;
    private const int MIN_NUMBER_OF_PLAYERS = 2;

    private readonly List<Deal> _deals = new();
    private readonly List<GamePlayer> _players = new();
    private readonly GamePlayer _lastStartingPlayer = null;

    public GameId Id { get; }

    public Game(GameId id, Croupier croupier)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        _croupier = croupier ?? throw new ArgumentNullException(nameof(croupier));
    }

    public GamePlayer Join(PlayerNick nick)
    {
        if (IsGameStarted())
            throw new JoinGameThatIsAlreadyStartedException(Id, nick);

        if (_players.Count >= MAX_NUMBER_OF_PLAYERS)
            throw new MaxGamePlayersReachedException(Id);

        if (_players.Exists(player => player.Nick == nick))
            throw new PlayerAlreadyJoinedTheGameException(Id, nick);

        var joiningSequence = _players.Count + 1;
        var player = GamePlayer.Create(nick, joiningSequence);
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
        // todo: parametrize players in new deal based on last deal (number of cards)

        var nextDealNumber = _deals.Count + 1;
        var nextDealId = new DealId(Id, Number: new DealNumber(nextDealNumber));
        var nextDealPlayers = CreateNextDealPlayers();
        var deal = _croupier.Deal(nextDealId, nextDealPlayers);
        _deals.Add(deal);
        return nextDealId;
    }

    public Hand GetHand(PlayerId playerId, DealId dealId)
    {
        // todo: check if user exists
        // todo: check if deal number exits
        var deal = GetDeal(dealId);
        return deal.GetHand(playerId);
    }

    public void Bid(DealId dealId, Bid newBid)
    {
        var deal = GetDeal(dealId);
        deal.Bid(newBid);
    }

    public void Check(DealId dealId, PlayerId playerId)
    {
        var deal = GetDeal(dealId);
        var lastDealLooser = deal.Check(playerId);
        var gamePlayer = _players.Single(p => p.PlayerId.Id.Equals(lastDealLooser.PlayerId));
        gamePlayer.LostLastDeal();
    }

    public DealFlowResult GetDealFlow(DealId dealId)
    {
        var deal = GetDeal(dealId);
        return deal.GetDealFlow();
    }

    public GameFlowResult GetGameFlow() =>
        new(_players);

    private bool IsGameStarted() =>
        _deals.Count > 0;

    private Deal GetDeal(DealId dealId) =>
        _deals.Single(d => d.DealId == dealId);

    private NextDealPlayerSet CreateNextDealPlayers() =>
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
}