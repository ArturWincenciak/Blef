namespace Blef.Modules.Games.Api.Tests.Core.ValueObjects;

internal sealed record PlayerId
{
    public Guid Id { get; }

    public PlayerId(Guid id)
    {
        if (id == Guid.Empty) // todo: better exception
            throw new Exception("Invalid player ID");

        Id = id;
    }
}