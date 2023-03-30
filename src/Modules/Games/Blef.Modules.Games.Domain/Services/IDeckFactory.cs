using Blef.Modules.Games.Domain.ValueObjects.Cards;

namespace Blef.Modules.Games.Domain.Services;

internal interface IDeckFactory
{
    Deck Create();
}