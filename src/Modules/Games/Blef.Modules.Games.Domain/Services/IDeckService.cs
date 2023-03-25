using Blef.Modules.Games.Domain.ValueObjects;

namespace Blef.Modules.Games.Domain.Services;

public interface IDeckService
{
    Card[] DealCards(int count);
}