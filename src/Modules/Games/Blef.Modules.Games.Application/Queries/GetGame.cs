using Blef.Modules.Games.Domain.ValueObjects.Ids;
using Blef.Shared.Abstractions.Queries;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Queries;

public sealed record GetGame(GameId GameId) : IQuery<GetGame.Result>
{
    [UsedImplicitly]
    public sealed record Result(IEnumerable<Player> Players, IEnumerable<DealNumber> Deals) : IQueryResult;

    [UsedImplicitly]
    public sealed record Player(Guid PlayerId, string Nick);

    public sealed record DealNumber(int Number);
}