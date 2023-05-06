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
        IEnumerable<IDomainEvent> actual)
    {
        var checkPlaced = actual.Single(@event => @event is CheckPlaced) as CheckPlaced;
        Assert.Equal(expectedGameId, checkPlaced!.Game);
        Assert.Equal(expectedDealNumber, checkPlaced.Deal);
        Assert.Equal(expectedCheckingPlayer, checkPlaced.CheckingPlayer.Player);
        Assert.Equal(expectedLooser, checkPlaced.LooserPlayer.Player);
    }
}