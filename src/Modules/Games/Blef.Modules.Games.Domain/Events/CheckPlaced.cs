using Blef.Modules.Games.Domain.Model;
using Blef.Shared.Abstractions.Events;

namespace Blef.Modules.Games.Domain.Events;

internal sealed record CheckPlaced(
    GameId Game,
    DealNumber Deal,
    CheckingPlayer CheckingPlayer,
    LooserPlayer LooserPlayer) : IDomainEvent<CheckPlaced>;