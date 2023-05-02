using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.ValueObjects.Dto;

internal record GameFlowResult(IEnumerable<GamePlayer> Players, IEnumerable<DealId> Deals);