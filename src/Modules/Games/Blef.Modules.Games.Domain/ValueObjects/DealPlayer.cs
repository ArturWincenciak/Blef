namespace Blef.Modules.Games.Domain.Entities;

public sealed class DealPlayer
{
    public Guid Id { get; }
    public string Nick { get; set; }
    public int OrderNumber { get; }
    public int CardsCount { get; }
    public Card[] Cards { get; }
}