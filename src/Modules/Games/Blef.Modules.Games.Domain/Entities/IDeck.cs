namespace Blef.Modules.Games.Domain.Entities;

public interface IDeck
{
    Card DealCard();
    Card[] DealCards(int count);
}