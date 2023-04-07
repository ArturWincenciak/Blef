using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.Services;

internal sealed class DealFactory
{
    private readonly Referee _referee;

    public DealFactory(Referee referee) =>
        _referee = referee ?? throw new ArgumentNullException(nameof(referee));

    public Deal Create(DealId dealId, IEnumerable<DealPlayer> players) =>
        new (dealId, players, _referee);
}