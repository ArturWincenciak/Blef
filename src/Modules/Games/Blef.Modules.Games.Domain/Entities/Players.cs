using Blef.Modules.Games.Domain.Exceptions;

namespace Blef.Modules.Games.Domain.Entities;

public class Players
{
    private readonly List<Player> _players = new();
    private int _currentPlayerIndex;

    public int Count => _players.Count;

    public bool ContainsNick(string playerNick) =>
        _players.Any(player => player.Nick == playerNick);

    public void Add(Player player) => 
        _players.Add(player);

    public Player GetPlayer(Guid playerId) => 
        _players.First(x => x.Id == playerId);

    public IEnumerable<Player> GetPlayers() =>
        _players.ToArray();

    public Player GetPreviousPlayer()
    {
        var previousPlayerIndex = _currentPlayerIndex - 1;
        if (previousPlayerIndex < 0)
        {
            previousPlayerIndex = _players.Count - 1;
        }
        
        return _players[previousPlayerIndex];
    }

    private Player GetCurrentPlayer() => 
        _players[_currentPlayerIndex];

    public void Bid(Guid playerId, string pokerHand)
    {
        var currentPlayer = GetCurrentPlayer();
        
        if (currentPlayer.Id != playerId)
        {
            throw new ThatIsNotThisPlayerTurnNowException(playerId);
        }
        
        currentPlayer.Bid(pokerHand);
        
        _currentPlayerIndex++;
        if (_currentPlayerIndex >= _players.Count)
        {
            _currentPlayerIndex = 0;
        }
    }
}