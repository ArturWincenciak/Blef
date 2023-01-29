using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.Exceptions;

namespace Blef.Modules.Games.Domain.Tests;

public class GameTests
{
    private const string KING = "one-of-a-kind:King";
    private const string ACE = "one-of-a-kind:Ace";
    private const string FIRST_PLAYER_NICK = "First Player Nick";
    private const string SECOND_PLAYER_NICK = "Second Player Nick";

    private readonly Game _game;
    private readonly Guid _firstPlayerId;
    private readonly Guid _secondPlayerId;

    public GameTests()
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
    public void Should_Accept_Higher_Bid()
    {
        // act
        var exception = Record.Exception(() =>
        {
            _game.Bid(_firstPlayerId, KING);
            _game.Bid(_secondPlayerId, ACE);
        });

        // assert
        Assert.Null(exception);
    }

    [Fact]
    public void Should_Not_Accept_Lower_Bid()
    {
        // arrange
        _game.Bid(_firstPlayerId, ACE);

        // assert
        Assert.Throws<BidIsNotHigherThenLastOneException>(() =>
        {
            // act
            _game.Bid(_firstPlayerId, KING);
        });
    }

    [Fact]
    public void Should_Not_Accept_The_Same_Bid()
    {
        // arrange
        _game.Bid(_firstPlayerId, KING);

        // assert
        Assert.Throws<BidIsNotHigherThenLastOneException>(() =>
        {
            // act
            _game.Bid(_firstPlayerId, KING);
        });
    }

    [Fact]
    public void Should_Not_Join_Game_After_It_Was_Started()
    {
        // arrange
        _game.Bid(_firstPlayerId, KING);

        // assert
        Assert.Throws<JoinGameThatIsAlreadyStartedException>(() =>
        {
            // act
            _game.Join("Next Player Nick");
        });
    }
}