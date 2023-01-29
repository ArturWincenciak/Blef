using Blef.Modules.Games.Api.Tests.Core;
using Blef.Modules.Games.Application.Queries;

namespace Blef.Modules.Games.Api.Tests;

public class HappyPathGameTests
{
    [Fact]
    public async Task PlayGame()
    {
        await new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Knuth)
            .GetCards(WhichPlayer.Knuth, with: AssertCards())
            .JoinPlayer(WhichPlayer.Graham)
            .GetCards(WhichPlayer.Knuth, with: AssertCards())
            .GetCards(WhichPlayer.Graham, with: AssertCards())
            .Bid(WhichPlayer.Knuth, "one-of-a-kind:nine")
            .Bid(WhichPlayer.Graham, "one-of-a-kind:ten")
            .Check(WhichPlayer.Knuth)
            .GetGameFlow()
            .Build();

        Action<GetPlayerCards.Result> AssertCards() => result =>
        {
            Assert.Single(result.Cards);
            Assert.NotEmpty(result.Cards[0].FaceCard);
            Assert.NotEmpty(result.Cards[0].Suit);
        };
    }
}