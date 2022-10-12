namespace Blef.Modules.Games.Domain.Entities;

public class Tournament
{
    private Tournament(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }

    public static Tournament Create() =>
        new(Guid.NewGuid());

    public void Join(Guid playerId)
    {
        throw new NotImplementedException();
    }
}