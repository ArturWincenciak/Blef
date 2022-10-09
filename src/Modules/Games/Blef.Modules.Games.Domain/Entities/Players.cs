namespace Blef.Modules.Games.Domain.Entities;

public class Players
{
    private readonly List<Player> _players = new();
    private int _currentPlayerIndex = 0;

    public int Count => _players.Count;

    public bool ContainsId(Guid playerId)
    {
        return _players.Any(x => x.Id == playerId);
    }

    public void Add(Player player)
    {
        _players.Add(player);
    }

    public Player GetPlayer(Guid playerId)
    {
        return _players.First(x => x.Id == playerId);
    }

    public Player GetPreviousPlayer()
    {
        var previousPlayerIndex = _currentPlayerIndex - 1;
        if (previousPlayerIndex < 0)
        {
            previousPlayerIndex = _players.Count - 1;
        }
        
        return _players[previousPlayerIndex];
    }

    public Player GetCurrentPlayer()
    {
        return _players[_currentPlayerIndex];
    }

    public void Bid(Guid playerId, string pokerHand)
    {
        if (GetCurrentPlayer().Id != playerId)
        {
            throw new Exception($"Player '{playerId}' should wait for his turn");
        }
        
        GetCurrentPlayer().Bid(pokerHand);
        _currentPlayerIndex++;
        if (_currentPlayerIndex >= _players.Count)
        {
            _currentPlayerIndex = 0;
        }
    }
}