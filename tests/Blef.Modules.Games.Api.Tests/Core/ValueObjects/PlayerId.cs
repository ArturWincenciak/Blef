namespace Blef.Modules.Games.Api.Tests.Core.ValueObjects;

internal class PlayerId
{
    internal Guid Id { get; }

    public PlayerId(Guid id)
    {
        if (id == Guid.Empty) // todo: better exception
            throw new Exception("Invalid player ID");

        Id = id;
    }
}