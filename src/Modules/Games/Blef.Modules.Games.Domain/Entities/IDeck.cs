namespace Blef.Modules.Games.Domain.Entities;

public interface IDeck
{
    Card[] DealCards(int count);
}