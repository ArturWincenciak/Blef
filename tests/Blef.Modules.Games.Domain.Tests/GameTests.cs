using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.Exceptions;

namespace Blef.Modules.Games.Domain.Tests;

public class GameTests
{
    private const string KING = "one-of-a-kind:King";
    private const string ACE = "one-of-a-kind:Ace";
    private const string PLAYER_NICK = "Player Nick";

    private readonly Game _game;
    private readonly Guid _playerId;

    public GameTests()
    {
        var deckStub = new DeckStub(new Card[]
        {
            new(FaceCard.King, Suit.Diamonds)
        });
        _game = Game.Create(deckStub);
        var player = _game.Join(PLAYER_NICK);
        _playerId = player.Id;
    }

    [Fact]
    public void Should_accept_higher_bid()
    {
        // act
        var exception = Record.Exception(() =>
        {
            _game.Bid(_playerId, KING);
            _game.Bid(_playerId, ACE);
        });

        // assert
        Assert.Null(exception);
    }

    [Fact]
    public void Should_not_accept_lower_bid()
    {
        _game.Bid(_playerId, ACE);
        Assert.Throws<BidIsNotHigherThenLastOneException>(() => _game.Bid(_playerId, KING));
    }

    [Fact]
    public void Should_not_accept_the_same_bid()
    {
        _game.Bid(_playerId, KING);
        Assert.Throws<BidIsNotHigherThenLastOneException>(() => _game.Bid(_playerId, KING));
    }

    [Fact]
    public void Should_not_join_game_after_it_was_started()
    {
        _game.Bid(_playerId, KING);

        const string nextPlayerNick = "Next Player Nick";
        Assert.Throws<JoinGameThatIsAlreadyStartedException>(() => _game.Join(nextPlayerNick));
    }
}