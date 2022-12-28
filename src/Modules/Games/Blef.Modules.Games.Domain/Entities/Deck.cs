namespace Blef.Modules.Games.Domain.Entities;

using System;
using System.Collections.Generic;

public class Deck : IDeck
{
    private readonly RandomnessProvider _randomnessProvider;
    private readonly List<Card> _cards;

    public Deck(RandomnessProvider randomnessProvider, List<Card> cards)
    {
        _randomnessProvider = randomnessProvider;
        _cards = cards;
    }

    public Card[] DealCards(int count)
    {
        List<Card> cards = new();
        for (var i = 0; i < count; i++)
        {
            cards.Add(DealCard());
        }

        return cards.ToArray();
    }

    public Card DealCard()
    {
        if (_cards.Any() == false)
        {
            throw new InvalidOperationException("Cannot deal more cards from deck. Deck is empty");
        }

        var randomPosition = _randomnessProvider.GetInt(_cards.Count);

        var card = _cards[randomPosition];

        _cards.RemoveAt(randomPosition);

        return card;
    }
}