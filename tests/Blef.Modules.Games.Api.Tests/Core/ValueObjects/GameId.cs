namespace Blef.Modules.Games.Api.Tests.Core.ValueObjects;

internal sealed record GameId
{
    public Guid Id { get; }

    public GameId(Guid id)
    {
        if (id == Guid.Empty) // todo: exception
            throw new Exception("TBD");

        Id = id;
    }
}