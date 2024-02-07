using Blef.Modules.Games.Domain.Model;
using Blef.Shared.Abstractions.Events;

namespace Blef.Modules.Games.Domain.Events;

internal sealed record DealStarted(
    GameId Game,
    DealNumber Deal,
    IReadOnlyCollection<DealPlayer> Players)
    : IDomainEvent<DealStarted>;