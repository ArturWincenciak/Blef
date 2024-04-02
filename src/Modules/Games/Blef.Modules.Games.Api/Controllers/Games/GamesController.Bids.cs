using Blef.Modules.Games.Api.Controllers.Games.Commands;
using Blef.Modules.Games.Api.Controllers.Games.Queries;
using Blef.Modules.Games.Application.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blef.Modules.Games.Api.Controllers.Games;

internal sealed partial class GamesController
{
    [Tags("Games' bids")]
    [HttpPost(HighCardBidsRoute.ROUTE)]
    public async Task<IActionResult> BidHighCard(
        [FromRoute] HighCardBidsRoute route,
        [FromBody] HighCardBidPayload payload,
        CancellationToken cancellation) =>
        await Bid(route, payload, cancellation);

    [Tags("Games' bids")]
    [HttpPost(PairBidsRoute.ROUTE)]
    public async Task<IActionResult> BidPair(
        [FromRoute] PairBidsRoute route,
        [FromBody] PairBidPayload payload,
        CancellationToken cancellation) =>
        await Bid(route, payload, cancellation);

    [Tags("Games' bids")]
    [HttpPost(TwoPairsBidsRoute.ROUTE)]
    public async Task<IActionResult> BidTwoPairs(
        [FromRoute] TwoPairsBidsRoute route,
        [FromBody] TwoPairsBidPayload payload,
        CancellationToken cancellation) =>
        await Bid(route, payload, cancellation);

    [Tags("Games' bids")]
    [HttpPost(LowStraightBidsRoute.ROUTE)]
    public async Task<IActionResult> BidLowStraight(
        [FromRoute] LowStraightBidsRoute route,
        CancellationToken cancellation) =>
        await Bid(route, payload: new LowStraightBidPayload(), cancellation);

    [Tags("Games' bids")]
    [HttpPost(HighStraightBidsRoute.ROUTE)]
    public async Task<IActionResult> BidHighStraight(
        [FromRoute] HighStraightBidsRoute route,
        CancellationToken cancellation) =>
        await Bid(route, payload: new HighStraightBidPayload(), cancellation);

    [Tags("Games' bids")]
    [HttpPost(ThreeOfAKindBidsRoute.ROUTE)]
    public async Task<IActionResult> BidThreeOfAKind(
        [FromRoute] ThreeOfAKindBidsRoute route,
        [FromBody] ThreeOfAKindBidPayload payload,
        CancellationToken cancellation) =>
        await Bid(route, payload, cancellation);

    [Tags("Games' bids")]
    [HttpPost(FullHouseBidsRoute.ROUTE)]
    public async Task<IActionResult> BidFullHouse(
        [FromRoute] FullHouseBidsRoute route,
        [FromBody] FullHouseBidPayload payload,
        CancellationToken cancellation) =>
        await Bid(route, payload, cancellation);

    [Tags("Games' bids")]
    [HttpPost(FlushBidsRoute.ROUTE)]
    public async Task<IActionResult> BidFlush(
        [FromRoute] FlushBidsRoute route,
        [FromBody] FlushBidPayload payload,
        CancellationToken cancellation) =>
        await Bid(route, payload, cancellation);

    [Tags("Games' bids")]
    [HttpPost(FourOfAKindBidsRoute.ROUTE)]
    public async Task<IActionResult> BidFourOfAKind(
        [FromRoute] FourOfAKindBidsRoute route,
        [FromBody] FourOfAKindBidPayload payload,
        CancellationToken cancellation) =>
        await Bid(route, payload, cancellation);

    [Tags("Games' bids")]
    [HttpPost(StraightFlushBidsRoute.ROUTE)]
    public async Task<IActionResult> BidStraightFlush(
        [FromRoute] StraightFlushBidsRoute route,
        [FromBody] StraightFlushBidPayload payload,
        CancellationToken cancellation) =>
        await Bid(route, payload, cancellation);

    [Tags("Games' bids")]
    [HttpPost(RoyalFlushBidsRoute.ROUTE)]
    public async Task<IActionResult> BidRoyalFlush(
        [FromRoute] RoyalFlushBidsRoute route,
        [FromBody] RoyalFlushBidPayload payload,
        CancellationToken cancellation) =>
        await Bid(route, payload, cancellation);

    private async Task<IActionResult> Bid(BidsRoute route, BidPayload payload, CancellationToken cancellation)
    {
        var cmd = new Bid(route.GameId, route.PlayerId, PokerHand: payload.Serialize());
        var bid = await _commandDispatcher.Dispatch<Bid, Bid.Result>(cmd, cancellation);
        return Created(uri: GetDealFlowQuery.Path(route.GameId, bid.DealNumber), value: null);
    }
}