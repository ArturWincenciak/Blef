using Blef.Modules.Games.Domain.Entities;

namespace Blef.Modules.Games.Domain.Tests;

public class TwoPlayersGameTests
{
    private const string KING = "one-of-a-kind:King";
    private const string JACK = "one-of-a-kind:Jack";
    private const string FIRST_PLAYER_NICK = "First Player Nick";
    private const string SECOND_PLAYER_NICK = "Second Player Nick";
    private readonly Guid _firstPlayerId;

    private readonly Game _game;
    private readonly Guid _secondPlayerId;

    public TwoPlayersGameTests()
    {
        var deckStub = new DeckStub(new Card[]
        {
            new(FaceCard.King, Suit.Diamonds),
            new(FaceCard.Ace, Suit.Diamonds)
        });
        _game = Game.Create(deckStub);
        var firstPlayer = _game.Join(FIRST_PLAYER_NICK);
        _firstPlayerId = firstPlayer.Id;
        var secondPlayer = _game.Join(SECOND_PLAYER_NICK);
        _secondPlayerId = secondPlayer.Id;
    }

    [Fact]
    public void When_bid_was_on_the_table_then_checking_player_has_lost()
    {
        _game.Bid(_firstPlayerId, KING);
        _game.Check(_secondPlayerId);

        Assert.Equal(_secondPlayerId, _game.GetLooser());
    }

    [Fact]
    public void When_bid_was_NOT_on_the_table_then_bidding_player_has_lost()
    {
        _game.Bid(_firstPlayerId, JACK);
        _game.Check(_secondPlayerId);

        Assert.Equal(_firstPlayerId, _game.GetLooser());
    }
}