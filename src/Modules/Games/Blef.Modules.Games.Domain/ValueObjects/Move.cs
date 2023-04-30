using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.ValueObjects;

// todo: validation
internal sealed record Move(PlayerId Player, Order Order);