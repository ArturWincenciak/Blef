namespace Blef.Modules.Games.Domain.Entities;

public interface IDeckService
{
    Card[] DealCards(int count);
}