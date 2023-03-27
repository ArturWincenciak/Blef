using Blef.Modules.Games.Domain.Exceptions;
using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Cards;
using Blef.Modules.Games.Domain.ValueObjects.Dto;
using Blef.Modules.Games.Domain.ValueObjects.Ids;
using Blef.Modules.Games.Domain.ValueObjects.PokerHands;

namespace Blef.Modules.Games.Domain.Entities;

internal sealed class Game
{
    public GameId Id { get; }

    private const int MAX_NUMBER_OF_PLAYERS = 4;
    private const int MIN_NUMBER_OF_PLAYERS = 2;

    private readonly List<GamePlayer> _players = new();
    private readonly List<Deal> _deals = new();

    private Game(GameId id) =>
        Id = id ?? throw new ArgumentNullException(nameof(id));

    public static Game Create() =>
        new(id: new(Guid.NewGuid()));

    public GamePlayer Join(PlayerNick nick)
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
        // todo: parametrize players in new deal based on last deal (number of cards)

        var number = _deals.Count + 1;
        var dealId = new DealId(Id, new (number));
        var newDealPlayers = _players
            .Where(p => p.IsInTheGame)
            .Select(p => new NewDealPlayer(p.PlayerId, p.CardsAmount));
        var deal = Deal.Create(dealId, newDealPlayers);
        _deals.Add(deal);
        return dealId;
    }

    private bool IsGameStarted() =>
        _deals.Count > 0;

    public IEnumerable<Card> GetCards(PlayerId playerId, DealNumber dealNumber)
    {
        // todo: check if user exists
        // todo: check if deal number exits

        var deal = GetDeal(dealNumber);
        var cards = deal.GetCards(playerId);
        return cards;
    }

    public void Bid(DealNumber dealNumber, PlayerId playerId, PokerHand bid)
    {
        var deal = GetDeal(dealNumber);
        deal.Bid(playerId, bid);
    }

    public void Check(DealNumber dealNumber, PlayerId playerId)
    {
        var deal = GetDeal(dealNumber);
        var lastDealLooser = deal.Check(playerId);
        var gamePlayer = _players.Single(p => p.PlayerId.Equals(lastDealLooser.PlayerId));
        gamePlayer.OnLostLastDeal();
    }

    private Deal GetDeal(DealNumber dealNumber) =>
        _deals.Single(d => d.DealId.Number.Equals(dealNumber));

    public DealFlowResult GetDealFlow(DealNumber dealNumber)
    {
        var deal = GetDeal(dealNumber);
        return deal.GetDealFlow();
    }

    public GameFlowResult GetGameFlow() =>
        new GameFlowResult(_players);
}