namespace Blef.Modules.Games.Api.Tests.Core.ValueObjects;

internal class GameId
{
    internal Guid Id { get; }

    public GameId(Guid id)
    {
        if (id == Guid.Empty)
            throw new Exception("Invalid game ID");

        Id = id;
    }
}