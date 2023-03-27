using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.Entities;

internal sealed class GamePlayer
{
    public PlayerId PlayerId { get; }
    public PlayerNick Nick { get; }
    public CardsAmount CardsAmount { get; }
    public bool IsOutOfTheGame { get; private set; }
    public bool IsInTheGame => !IsOutOfTheGame;


    private GamePlayer(PlayerId playerId, PlayerNick nick, CardsAmount cardsAmount, bool isOutOfTheGame)
    {
        PlayerId = playerId ?? throw new ArgumentNullException(nameof(playerId));
        Nick = nick ?? throw new ArgumentNullException(nameof(nick));
        CardsAmount = cardsAmount;
        IsOutOfTheGame = isOutOfTheGame;
    }

    public static GamePlayer Create(PlayerNick nick) =>
        new (new(Guid.NewGuid()), nick, new CardsAmount(), isOutOfTheGame: false);

    public void OnLostLastDeal()
    {
        if (IsOutOfTheGame)
            throw new Exception("TODO: Define exception");

        if (CardsAmount.Value < CardsAmount.MAX_CARDS_AMOUNT)
            CardsAmount.AddOneCard();
        else
            IsOutOfTheGame = true;
    }
}