using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.Entities;

internal sealed class GamePlayer
{
    private bool _isOutOfTheGame;
    public PlayerId PlayerId { get; }
    public PlayerNick Nick { get; }
    public CardsAmount CardsAmount { get; }
    public bool IsInTheGame => !_isOutOfTheGame;

    private GamePlayer(PlayerId playerId, PlayerNick nick, CardsAmount cardsAmount)
    {
        PlayerId = playerId ?? throw new ArgumentNullException(nameof(playerId));
        Nick = nick ?? throw new ArgumentNullException(nameof(nick));
        CardsAmount = cardsAmount ?? throw new ArgumentNullException(nameof(cardsAmount));
        _isOutOfTheGame = false;
    }

    public static GamePlayer Create(PlayerNick nick) =>
        new(playerId: new PlayerId(Guid.NewGuid()), nick, CardsAmount.Initial);

    public void OnLostLastDeal()
    {
        if (_isOutOfTheGame) // todo: exception
            throw new Exception("TBD");

        if (CardsAmount < CardsAmount.Max)
            CardsAmount.AddOneCard();
        else
            _isOutOfTheGame = true;
    }
}