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

    public Card DealCard()
    {
        if (_cards.Any() == false)
        {
            throw new InvalidOperationException("Cannot deal more cards from deck. Deck is empty");
        }

        int randomPosition = _randomnessProvider.GetInt(_cards.Count);

        Card card = _cards[randomPosition];

        _cards.RemoveAt(randomPosition);

        return card;
    }
}