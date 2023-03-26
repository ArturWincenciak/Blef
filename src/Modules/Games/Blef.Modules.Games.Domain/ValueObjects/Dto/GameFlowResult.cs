using Blef.Modules.Games.Domain.Entities;

namespace Blef.Modules.Games.Domain.ValueObjects.Dto;

internal record GameFlowResult(IEnumerable<GamePlayer> Players);