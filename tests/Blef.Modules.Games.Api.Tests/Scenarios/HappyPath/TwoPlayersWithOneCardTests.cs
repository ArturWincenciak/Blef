using Blef.Modules.Games.Api.Tests.Core;
using Blef.Modules.Games.Api.Tests.Core.ValueObjects;
using Blef.Modules.Games.Application.Queries;
using Blef.Modules.Games.Domain.Entities;

namespace Blef.Modules.Games.Api.Tests.Scenarios.HappyPath;

public class TwoPlayersWithOneCardTests
{
    [Fact]
    public async Task OneBidByEachAndCheck()
    {
        await new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Knuth)
            .JoinPlayer(WhichPlayer.Graham)
            .JoinPlayer(WhichPlayer.Conway)
            // .Deal(WhichPlayer.Knuth)
            // .GetCards(WhichPlayer.Knuth, deal: new(1))
            // .GetCards(WhichPlayer.Graham, deal: new (1))
            // .Bid(WhichPlayer.Knuth, deal: new (1), PokerHand.HighCard.Nine)
            // .Bid(WhichPlayer.Graham, deal: new (1), PokerHand.HighCard.Ten)
            // .Check(WhichPlayer.Knuth, deal: new (1))
            // .GetGameFlow()
            .Build();
    }
}