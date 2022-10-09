using Blef.Modules.Games.Domain.Entities;

namespace Blef.Modules.Games.Domain.Tests;

public class Two_players_GameTests
{
    // TODO: #77 update tests after: Deal cards from the Deck
    
    private const string King = "one-of-a-kind:King";
    private const string Jack = "one-of-a-kind:Jack";

    private readonly Game _game = Game.Create();
    private readonly Guid _firstPlayer = Guid.NewGuid();
    private readonly Guid _secondPlayer = Guid.NewGuid();

    public Two_players_GameTests()
    {
        _game.Join(_firstPlayer);
        _game.Join(_secondPlayer);
    }

    [Fact]
    public void When_bid_was_on_the_table_then_checking_player_has_lost()
    {
        _game.Bid(_firstPlayer, King);
        _game.Check(_secondPlayer);
        
        Assert.Equal(_secondPlayer, _game.GetLooser());
    }

    [Fact]
    public void When_bid_was_NOT_on_the_table_then_bidding_player_has_lost()
    {
        _game.Bid(_firstPlayer, Jack);
        _game.Check(_secondPlayer);
        
        Assert.Equal(_firstPlayer, _game.GetLooser());
    }

}