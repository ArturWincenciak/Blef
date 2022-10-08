namespace Blef.Modules.Games.Domain;

internal sealed class Games
{
    private readonly Dictionary<Guid, Game> _games = new();

    public Guid MakeNewGame()
    {
        var game = Game.Create();
        _games.Add(game.Id, game);
        return game.Id;
    }

    public Game GetExistingGame(Guid gameId) =>
        _games[gameId];
}