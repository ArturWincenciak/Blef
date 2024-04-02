using Blef.Modules.Games.Domain.Model;

namespace Blef.Modules.Games.Domain.Services;

internal sealed class GameFactory
{
    private readonly Croupier _croupier;

    public GameFactory(Croupier croupier) =>
        _croupier = croupier ?? throw new ArgumentNullException(nameof(croupier));

    public Game Create() =>
        new(id: new(Guid.NewGuid()), _croupier);
}