using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Cards;

namespace Blef.Modules.Games.Domain.Services;

internal interface IDeckService
{
    Card[] DealCards(int count);
}