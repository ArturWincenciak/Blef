using Blef.Modules.Games.Domain.Events;
using Blef.Modules.Games.Domain.Model;
using Blef.Shared.Abstractions.Events;

namespace Blef.Modules.Games.Domain.Tests.Extensions;

internal static class AssertExtension
{
    public static void AssertCheckPlaced(
        GameId expectedGameId,
        DealNumber expectedDealNumber,
        PlayerId expectedCheckingPlayer,
        PlayerId expectedLooser,
        IReadOnlyCollection<IDomainEvent> actual)
    {
        var checkPlaced = actual.Single(@event => @event is CheckPlaced) as CheckPlaced;
        Assert.Equal(expectedGameId, checkPlaced!.Game);
        Assert.Equal(expectedDealNumber, checkPlaced.Deal);
        Assert.Equal(expectedCheckingPlayer, checkPlaced.CheckingPlayer.Player);
        Assert.Equal(expectedLooser, checkPlaced.LooserPlayer.Player);
    }

    public static void AssertDealStarted(
        GameId expectedGameId,
        DealNumber expectedDealNumber,
        IReadOnlyCollection<PlayerId> expectedNextDealPlayers,
        IReadOnlyCollection<IDomainEvent> actual)
    {
        var dealStarted = actual.Single(@event => @event is DealStarted) as DealStarted;
        Assert.Equal(expectedGameId, dealStarted!.Game);
        Assert.Equal(expectedDealNumber, dealStarted.Deal);
        Assert.Equal(expected: expectedNextDealPlayers.Count, actual: dealStarted.Players.Count);

        var nextDealPlayers = dealStarted.Players.Select(dealPlayer => dealPlayer.Player);
        foreach (var expectedNextDealPlayer in expectedNextDealPlayers)
            Assert.Contains(nextDealPlayers, filter: player => player == expectedNextDealPlayer);
    }
}