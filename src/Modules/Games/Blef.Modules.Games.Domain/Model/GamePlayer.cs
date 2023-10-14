namespace Blef.Modules.Games.Domain.Model;

internal sealed class GamePlayer
{
    private bool _isOutOfTheGame;
    public PlayerId Id { get; }
    public PlayerNick Nick { get; }
    public CardsAmount CardsAmount { get; private set; }
    public Order JoiningSequence { get; }
    public bool IsInTheGame => !_isOutOfTheGame;

    private GamePlayer(PlayerId playerId, PlayerNick nick, CardsAmount cardsAmount, Order joiningSequence)
    {
        Id = playerId ?? throw new ArgumentNullException(nameof(playerId));
        Nick = nick ?? throw new ArgumentNullException(nameof(nick));
        CardsAmount = cardsAmount ?? throw new ArgumentNullException(nameof(cardsAmount));
        _isOutOfTheGame = false;
        JoiningSequence = joiningSequence;
    }

    public static GamePlayer Create(PlayerNick nick, Order joiningSequence) =>
        new(playerId: new PlayerId(Guid.NewGuid()), nick, CardsAmount.Initial, joiningSequence);

    public void LostLastDeal()
    {
        if (_isOutOfTheGame)
            throw new InvalidOperationException("Player already is out of the game");

        if (CardsAmount.IsLowerThen(CardsAmount.Max))
            CardsAmount = CardsAmount.AddOneCard();
        else
            _isOutOfTheGame = true;
    }
}