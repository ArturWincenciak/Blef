namespace Blef.Modules.Games.Api.Tests.Core.ValueObjects;

internal sealed record PlayerId
{
    public Guid Id { get; }

    public PlayerId(Guid id)
    {
        if (id == Guid.Empty) // todo: exception
            throw new Exception("TBD");

        Id = id;
    }
}