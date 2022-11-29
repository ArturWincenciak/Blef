GenericPokerHand określa typ układu, kolejność jak poniżej od najmniej (1) do najbardziej ważnego (11).
Najpierw porównujemy więc GenericPokerHand. Gdy jest równy przechodzimy do porównania PokerHand.CompareWithinSameGenericPokerHand(PokerHand),
który jest implementowany dla każdego układu. Dla niektórych układów funkcja ta zwraca '0', bo nie już co porównywać, sam układ jest ważny.

1. HighCard - wysoka karta
2. Pair - para
3. TwoPairs - dwie pary
4. LowStraight - mały
5. HighStraight - duży
6. ThreeOfKind - trójka
7. FullHouse - full
8. Flush - kolor
9. FourOfKind - kareta
10. LowStraightFlush -  mały poker
11. HighStraightFlush - duży poker