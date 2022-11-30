using Blef.Modules.Games.Domain.Entities;

namespace Blef.Modules.Games.Domain.Tests;

public class TwoPlayersGameTests
{
    private const string King = "one-of-a-kind:King";
    private const string Jack = "one-of-a-kind:Jack";

    private readonly Game _game;
    private readonly Guid _firstPlayerId;
    private readonly string _firstPlayerNick = "First Player Nick";
    private readonly Guid _secondPlayerId;
    private readonly string _secondPlayerNick = "Second Player Nick";

    public TwoPlayersGameTests()
    {
        var deckStub = new DeckStub(new Card[]
        {
            new(FaceCard.King, Suit.Diamonds),
            new(FaceCard.Ace, Suit.Diamonds),
        });
        _game = Game.Create(deckStub);
        var firstPlayer = _game.Join(_firstPlayerNick);
        _firstPlayerId = firstPlayer.Id;
        var secondPlayer = _game.Join(_secondPlayerNick);
        _secondPlayerId = secondPlayer.Id;
    }

    [Fact]
    public void When_bid_was_on_the_table_then_checking_player_has_lost()
    {
        _game.Bid(_firstPlayerId, King);
        _game.Check(_secondPlayerId);
        
        Assert.Equal(_secondPlayerId, _game.GetLooser());
    }

    [Fact]
    public void When_bid_was_NOT_on_the_table_then_bidding_player_has_lost()
    {
        _game.Bid(_firstPlayerId, Jack);
        _game.Check(_secondPlayerId);
        
        Assert.Equal(_firstPlayerId, _game.GetLooser());
    }
}