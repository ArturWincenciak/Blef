using Blef.Modules.Games.Domain.Entities;
using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Domain.Tests;

public class GameTests
{
    private const string King = "one-of-a-kind:King";
    private const string Ace = "one-of-a-kind:Ace";

    private readonly Game _game;
    private readonly Guid _playerId = Guid.NewGuid();

    public GameTests()
    {
        var deckStub = new DeckStub(new Card[]
        {
            new(FaceCard.King, Suit.Diamonds),
        });
        _game = Game.Create(deckStub);
        _game.Join(_playerId);
    }

    [Fact]
    public void Should_accept_higher_bid()
    {
        _game.Bid(_playerId, King);
        _game.Bid(_playerId, Ace);
    }

    [Fact]
    public void Should_not_accept_lower_bid()
    {
        _game.Bid(_playerId, Ace);
        Assert.Throws<Exception>(() => _game.Bid(_playerId, King));
    }

    [Fact]
    public void Should_not_accept_the_same_bid()
    {
        _game.Bid(_playerId, King);
        Assert.Throws<Exception>(() => _game.Bid(_playerId, King));
    }

    [Fact]
    public void Should_not_join_game_after_it_was_started()
    {
        _game.Bid(_playerId, King);

        var nextPlayer = Guid.NewGuid();
        Assert.Throws<SimpleBlefException>(() => _game.Join(nextPlayer));
    }
}