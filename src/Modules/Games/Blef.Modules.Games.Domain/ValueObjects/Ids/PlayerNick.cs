namespace Blef.Modules.Games.Domain.ValueObjects.Ids;

public sealed record PlayerNick
{
    public string Nick { get; }

    public PlayerNick(string nick)
    {
        if (string.IsNullOrWhiteSpace(nick))
            throw new ArgumentException("Nick cannot be empty");

        Nick = nick;
    }
}