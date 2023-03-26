using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.ValueObjects.Dto;

internal record GameFlowResult(IEnumerable<GameFlowResult.Player> Players)
{
    internal record Player(PlayerId PlayerId, string Nick);
}