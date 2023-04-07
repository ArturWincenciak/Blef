using Blef.Modules.Games.Domain.Services;
using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Cards;
using Blef.Modules.Games.Domain.ValueObjects.Dto;
using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.Entities;

internal sealed class Deal
{
    private readonly Referee _referee;
    private readonly BidHistory _bidHistory;

    private readonly IEnumerable<DealPlayer> _players;

    private Bid _lastBid;
    private CheckingPlayer _checkingPlayer;
    private LooserPlayer _looserPlayer;

    public DealId DealId { get; }

    public Deal(DealId dealId, IEnumerable<DealPlayer> players, Referee referee)
    {
        // todo: validate if here are at least two players
        // todo: validate if here are not more then four players
        DealId = dealId ?? throw new ArgumentNullException(nameof(dealId));
        _players = players ?? throw new ArgumentNullException(nameof(players));
        _referee = referee ?? throw new ArgumentNullException(nameof(referee));
        _bidHistory = new BidHistory();
        _looserPlayer = new LooserPlayer();
        _checkingPlayer = new CheckingPlayer();
    }

    public IEnumerable<Card> GetCards(PlayerId playerId)
    {
        var player = _players.Single(p => p.PlayerId.Equals(playerId));
        return player.Cards;
    }

    public void Bid(Bid newBid)
    {
        var higherBid = _referee.GetHigherBid(newBid.PokerHand, _lastBid.PokerHand);
        var isNewBidHigher = newBid.Equals(higherBid);
        if(isNewBidHigher == false)
            throw new Exception("TBD"); // todo: exception

        _lastBid = newBid;
        _bidHistory.OnBid(newBid);
    }

    public LooserPlayer Check(PlayerId checkingPlayerId)
    {
        _checkingPlayer = new CheckingPlayer(checkingPlayerId.Id);

        var allPlayersCards = _players.SelectMany(player => player.Cards);
        var lastBidIsInTheHandsOfPlayers = _referee.ContainsPokerHand(allPlayersCards, _lastBid.PokerHand);
        if (lastBidIsInTheHandsOfPlayers)
            _looserPlayer = new LooserPlayer(checkingPlayerId.Id);
        else
            _looserPlayer = new LooserPlayer(_lastBid.Player.Id);

        return _looserPlayer;
    }

    public DealFlowResult GetDealFlow()
    {
        var bids = _bidHistory.GetFlow();
        return new DealFlowResult(_players, bids, _checkingPlayer, _looserPlayer);
    }
}