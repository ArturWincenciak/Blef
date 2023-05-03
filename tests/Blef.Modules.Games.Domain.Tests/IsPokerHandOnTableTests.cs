﻿using System.Collections;
using Blef.Modules.Games.Domain.ValueObjects.Cards;
using Blef.Modules.Games.Domain.ValueObjects.PokerHands;
using static Blef.Modules.Games.Domain.Tests.Extensions.PokerHandFactory;

namespace Blef.Modules.Games.Domain.Tests;

// todo: unit tests: IsNotPokerHandOnTableTests
public class IsPokerHandOnTableTests
{
    [Theory]
    [ClassData(typeof(GivenPokerHandThatIsOnTheTable))]
    internal void PokerHandIsOnTableTest(Table table, PokerHand pokerHand)
    {
        // act
        var actual = pokerHand.IsOnTable(table);

        // assert
        Assert.True(actual);
    }

    // arrange
    private class GivenPokerHandThatIsOnTheTable : IEnumerable<object[]>
    {
        private static IEnumerable<object[]> Init =>
            new List<object[]>();

        private static IEnumerable<object[]> HighCardTestCases =>
            new List<object[]>
            {
                new object[]
                {
                    GivenTable(new Hand[]
                    {
                        new(new[] {new Card(FaceCard.Ace, Suit.Clubs)}),
                        new(new[] {new Card(FaceCard.King, Suit.Diamonds)})
                    }),
                    GivenHighCard(FaceCard.Ace)
                },
                new object[]
                {
                    GivenTable(new Hand[]
                    {
                        new(new[] {new Card(FaceCard.Ace, Suit.Clubs)}),
                        new(new[] {new Card(FaceCard.King, Suit.Hearts)})
                    }),
                    GivenHighCard(FaceCard.King)
                },
                new object[]
                {
                    GivenTable(new Hand[]
                    {
                        new(new[] {new Card(FaceCard.Queen, Suit.Spades)}),
                        new(new[] {new Card(FaceCard.King, Suit.Hearts)})
                    }),
                    GivenHighCard(FaceCard.Queen)
                }
            };

        private static IEnumerable<object[]> PairTestCases =>
            new List<object[]>
            {
                new object[]
                {
                    GivenTable(new Hand[]
                    {
                        new(new[] {new Card(FaceCard.Nine, Suit.Spades)}),
                        new(new[] {new Card(FaceCard.Nine, Suit.Diamonds)})
                    }),
                    GivenPair(FaceCard.Nine)
                },
                new object[]
                {
                    GivenTable(new Hand[]
                    {
                        new(new[] {new Card(FaceCard.Ten, Suit.Diamonds)}),
                        new(new[] {new Card(FaceCard.King, Suit.Clubs)}),
                        new(new[] {new Card(FaceCard.Ten, Suit.Spades)})
                    }),
                    GivenPair(FaceCard.Ten)
                },
                new object[]
                {
                    GivenTable(new Hand[]
                    {
                        new(new[] {new Card(FaceCard.Ace, Suit.Diamonds)}),
                        new(new[] {new Card(FaceCard.Jack, Suit.Spades)}),
                        new(new[] {new Card(FaceCard.Jack, Suit.Hearts)}),
                        new(new[] {new Card(FaceCard.Ace, Suit.Hearts)})
                    }),
                    GivenPair(FaceCard.Ace)
                },
                new object[]
                {
                    GivenTable(new Hand[]
                    {
                        new(new[] {new Card(FaceCard.Ace, Suit.Diamonds)}),
                        new(new[] {new Card(FaceCard.Ace, Suit.Spades)}),
                        new(new[] {new Card(FaceCard.Ace, Suit.Hearts)}),
                        new(new[] {new Card(FaceCard.Ace, Suit.Clubs)})
                    }),
                    GivenPair(FaceCard.Ace)
                }
            };

        private static IEnumerable<object[]> TwoPairsTestCases =>
            new List<object[]>
            {
                new object[]
                {
                    GivenTable(new Hand[]
                    {
                        new(new[]
                        {
                            new Card(FaceCard.Nine, Suit.Spades),
                            new Card(FaceCard.Ten, Suit.Clubs)
                        }),
                        new(new[]
                        {
                            new Card(FaceCard.Nine, Suit.Clubs),
                            new Card(FaceCard.Ace, Suit.Diamonds)
                        }),
                        new(new[]
                        {
                            new Card(FaceCard.Jack, Suit.Diamonds)
                        }),
                        new(new[]
                        {
                            new Card(FaceCard.Ace, Suit.Hearts)
                        })
                    }),
                    GivenTwoPairs(FaceCard.Nine, FaceCard.Ace)
                },
                new object[]
                {
                    GivenTable(new Hand[]
                    {
                        new(new[]
                        {
                            new Card(FaceCard.Nine, Suit.Diamonds),
                            new Card(FaceCard.Ten, Suit.Diamonds)
                        }),
                        new(new[]
                        {
                            new Card(FaceCard.King, Suit.Clubs),
                            new Card(FaceCard.King, Suit.Hearts)
                        }),
                        new(new[]
                        {
                            new Card(FaceCard.Ten, Suit.Spades)
                        }),
                        new(new[]
                        {
                            new Card(FaceCard.Nine, Suit.Spades)
                        })
                    }),
                    GivenTwoPairs(FaceCard.King, FaceCard.Ten)
                },
                new object[]
                {
                    GivenTable(new Hand[]
                    {
                        new(new[] {new Card(FaceCard.Queen, Suit.Diamonds)}),
                        new(new[] {new Card(FaceCard.Jack, Suit.Spades)}),
                        new(new[] {new Card(FaceCard.Jack, Suit.Hearts)}),
                        new(new[] {new Card(FaceCard.Queen, Suit.Hearts)})
                    }),
                    GivenTwoPairs(FaceCard.Jack, FaceCard.Queen)
                },
                new object[]
                {
                    GivenTable(new Hand[]
                    {
                        new(new[]
                        {
                            new Card(FaceCard.King, Suit.Diamonds),
                            new Card(FaceCard.King, Suit.Spades)
                        }),
                        new(new[]
                        {
                            new Card(FaceCard.King, Suit.Clubs),
                            new Card(FaceCard.King, Suit.Hearts)
                        }),
                        new(new[]
                        {
                            new Card(FaceCard.Ten, Suit.Spades)
                        }),
                        new(new[]
                        {
                            new Card(FaceCard.Ten, Suit.Hearts)
                        })
                    }),
                    GivenTwoPairs(FaceCard.King, FaceCard.Ten)
                }
            };

        private static IEnumerable<object[]> LowStraightTestCases =>
            new List<object[]>
            {
                new object[]
                {
                    GivenTable(new Hand[]
                    {
                        new(new[]
                        {
                            new Card(FaceCard.Nine, Suit.Spades),
                            new Card(FaceCard.Ten, Suit.Clubs)
                        }),
                        new(new[]
                        {
                            new Card(FaceCard.Jack, Suit.Clubs)
                        }),
                        new(new[]
                        {
                            new Card(FaceCard.Queen, Suit.Diamonds)
                        }),
                        new(new[]
                        {
                            new Card(FaceCard.King, Suit.Diamonds)
                        })
                    }),
                    LowStraight.Create()
                },
                new object[]
                {
                    GivenTable(new Hand[]
                    {
                        new(new[]
                        {
                            new Card(FaceCard.Ten, Suit.Diamonds),
                            new Card(FaceCard.Nine, Suit.Diamonds)
                        }),
                        new(new[]
                        {
                            new Card(FaceCard.Queen, Suit.Hearts),
                            new Card(FaceCard.Ace, Suit.Spades)
                        }),
                        new(new[]
                        {
                            new Card(FaceCard.Jack, Suit.Clubs)
                        }),
                        new(new[]
                        {
                            new Card(FaceCard.King, Suit.Spades)
                        })
                    }),
                    LowStraight.Create()
                },
                new object[]
                {
                    GivenTable(new Hand[]
                    {
                        new(new[]
                        {
                            new Card(FaceCard.Nine, Suit.Hearts),
                            new Card(FaceCard.Nine, Suit.Spades)
                        }),
                        new(new[]
                        {
                            new Card(FaceCard.Ten, Suit.Spades),
                            new Card(FaceCard.Jack, Suit.Spades)
                        }),
                        new(new[]
                        {
                            new Card(FaceCard.Jack, Suit.Diamonds),
                            new Card(FaceCard.Queen, Suit.Spades)
                        }),
                        new(new[]
                        {
                            new Card(FaceCard.King, Suit.Spades)
                        })
                    }),
                    LowStraight.Create()
                },
                new object[]
                {
                    GivenTable(new Hand[]
                    {
                        new(new[]
                        {
                            new Card(FaceCard.Nine, Suit.Spades),
                            new Card(FaceCard.Nine, Suit.Hearts)
                        }),
                        new(new[]
                        {
                            new Card(FaceCard.Ten, Suit.Spades),
                            new Card(FaceCard.Ten, Suit.Hearts)
                        }),
                        new(new[]
                        {
                            new Card(FaceCard.Jack, Suit.Spades),
                            new Card(FaceCard.Jack, Suit.Hearts),
                            new Card(FaceCard.King, Suit.Hearts)
                        }),
                        new(new[]
                        {
                            new Card(FaceCard.Queen, Suit.Spades),
                            new Card(FaceCard.Queen, Suit.Hearts),
                            new Card(FaceCard.King, Suit.Spades)
                        })
                    }),
                    LowStraight.Create()
                }
            };

        private static IEnumerable<object[]> HighStraightTestCases =>
            new List<object[]>
            {
                new object[]
                {
                    GivenTable(new Hand[]
                    {
                        new(new[]
                        {
                            new Card(FaceCard.Ten, Suit.Clubs),
                            new Card(FaceCard.Jack, Suit.Clubs)
                        }),
                        new(new[]
                        {
                            new Card(FaceCard.Queen, Suit.Diamonds)
                        }),
                        new(new[]
                        {
                            new Card(FaceCard.King, Suit.Diamonds)
                        }),
                        new(new[]
                        {
                            new Card(FaceCard.Ace, Suit.Spades)
                        })
                    }),
                    HighStraight.Create()
                },
                new object[]
                {
                    GivenTable(new Hand[]
                    {
                        new(new[]
                        {
                            new Card(FaceCard.Ten, Suit.Diamonds),
                            new Card(FaceCard.Nine, Suit.Diamonds)
                        }),
                        new(new[]
                        {
                            new Card(FaceCard.Queen, Suit.Hearts),
                            new Card(FaceCard.Ace, Suit.Spades)
                        }),
                        new(new[]
                        {
                            new Card(FaceCard.Jack, Suit.Clubs)
                        }),
                        new(new[]
                        {
                            new Card(FaceCard.King, Suit.Spades)
                        })
                    }),
                    HighStraight.Create()
                },
                new object[]
                {
                    GivenTable(new Hand[]
                    {
                        new(new[]
                        {
                            new Card(FaceCard.Ten, Suit.Spades),
                            new Card(FaceCard.Jack, Suit.Spades)
                        }),
                        new(new[]
                        {
                            new Card(FaceCard.Jack, Suit.Clubs),
                            new Card(FaceCard.Queen, Suit.Spades)
                        }),
                        new(new[]
                        {
                            new Card(FaceCard.King, Suit.Spades),
                            new Card(FaceCard.Ace, Suit.Hearts)
                        }),
                        new(new[]
                        {
                            new Card(FaceCard.Ace, Suit.Spades)
                        })
                    }),
                    HighStraight.Create()
                },
                new object[]
                {
                    GivenTable(new Hand[]
                    {
                        new(new[]
                        {
                            new Card(FaceCard.Ten, Suit.Spades),
                            new Card(FaceCard.King, Suit.Hearts),
                            new Card(FaceCard.King, Suit.Diamonds)
                        }),
                        new(new[]
                        {
                            new Card(FaceCard.Jack, Suit.Hearts),
                            new Card(FaceCard.King, Suit.Clubs),
                            new Card(FaceCard.Queen, Suit.Hearts)
                        }),
                        new(new[]
                        {
                            new Card(FaceCard.King, Suit.Spades),
                            new Card(FaceCard.Ace, Suit.Clubs),
                            new Card(FaceCard.Ace, Suit.Spades)
                        }),
                        new(new[]
                        {
                            new Card(FaceCard.Ace, Suit.Hearts)
                        })
                    }),
                    HighStraight.Create()
                },
                new object[]
                {
                    GivenTopTableWithMaxPlayersWhoEachHaveMaxCards(),
                    GivenHighCard(FaceCard.Ace)
                },
                new object[]
                {
                    GivenTopTableWithMaxPlayersWhoEachHaveMaxCards(),
                    GivenPair(FaceCard.Ace)
                },
                new object[]
                {
                    GivenTopTableWithMaxPlayersWhoEachHaveMaxCards(),
                    GivenTwoPairs(FaceCard.Ace, FaceCard.King)
                },
                new object[]
                {
                    GivenTopTableWithMaxPlayersWhoEachHaveMaxCards(),
                    HighStraight.Create()
                },
                new object[]
                {
                    GivenLowTableWithMaxPlayersWhoEachHaveMaxCards(),
                    GivenHighCard(FaceCard.Nine)
                },
                new object[]
                {
                    GivenLowTableWithMaxPlayersWhoEachHaveMaxCards(),
                    GivenPair(FaceCard.Nine)
                },
                new object[]
                {
                    GivenLowTableWithMaxPlayersWhoEachHaveMaxCards(),
                    GivenTwoPairs(FaceCard.Nine, FaceCard.Ten)
                },
                new object[]
                {
                    GivenLowTableWithMaxPlayersWhoEachHaveMaxCards(),
                    LowStraight.Create()
                }
            };

        private static Table GivenTopTableWithMaxPlayersWhoEachHaveMaxCards() =>
            GivenTable(new Hand[]
            {
                new(new[]
                {
                    new Card(FaceCard.Ace, Suit.Spades),
                    new Card(FaceCard.King, Suit.Spades),
                    new Card(FaceCard.Queen, Suit.Spades),
                    new Card(FaceCard.Jack, Suit.Spades),
                    new Card(FaceCard.Ten, Suit.Spades)
                }),
                new(new[]
                {
                    new Card(FaceCard.Ace, Suit.Diamonds),
                    new Card(FaceCard.King, Suit.Diamonds),
                    new Card(FaceCard.Queen, Suit.Diamonds),
                    new Card(FaceCard.Jack, Suit.Diamonds),
                    new Card(FaceCard.Ten, Suit.Diamonds)
                }),
                new(new[]
                {
                    new Card(FaceCard.Ace, Suit.Clubs),
                    new Card(FaceCard.King, Suit.Clubs),
                    new Card(FaceCard.Queen, Suit.Clubs),
                    new Card(FaceCard.Jack, Suit.Clubs),
                    new Card(FaceCard.Ten, Suit.Clubs)
                }),
                new(new[]
                {
                    new Card(FaceCard.Ace, Suit.Hearts),
                    new Card(FaceCard.King, Suit.Hearts),
                    new Card(FaceCard.Queen, Suit.Hearts),
                    new Card(FaceCard.Jack, Suit.Hearts),
                    new Card(FaceCard.Ten, Suit.Hearts)
                })
            });

        private static Table GivenLowTableWithMaxPlayersWhoEachHaveMaxCards() =>
            GivenTable(new Hand[]
            {
                new(new[]
                {
                    new Card(FaceCard.King, Suit.Spades),
                    new Card(FaceCard.Queen, Suit.Spades),
                    new Card(FaceCard.Jack, Suit.Spades),
                    new Card(FaceCard.Ten, Suit.Spades),
                    new Card(FaceCard.Nine, Suit.Spades)
                }),
                new(new[]
                {
                    new Card(FaceCard.King, Suit.Diamonds),
                    new Card(FaceCard.Queen, Suit.Diamonds),
                    new Card(FaceCard.Jack, Suit.Diamonds),
                    new Card(FaceCard.Ten, Suit.Diamonds),
                    new Card(FaceCard.Nine, Suit.Diamonds)
                }),
                new(new[]
                {
                    new Card(FaceCard.King, Suit.Clubs),
                    new Card(FaceCard.Queen, Suit.Clubs),
                    new Card(FaceCard.Jack, Suit.Clubs),
                    new Card(FaceCard.Ten, Suit.Clubs),
                    new Card(FaceCard.Nine, Suit.Clubs)
                }),
                new(new[]
                {
                    new Card(FaceCard.King, Suit.Hearts),
                    new Card(FaceCard.Queen, Suit.Hearts),
                    new Card(FaceCard.Jack, Suit.Hearts),
                    new Card(FaceCard.Ten, Suit.Hearts),
                    new Card(FaceCard.Nine, Suit.Hearts)
                })
            });

        public IEnumerator<object[]> GetEnumerator() =>
            GivenPokerHandTestCases().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();

        private static IEnumerable<object[]> GivenPokerHandTestCases() => Init
            .Concat(HighCardTestCases)
            .Concat(PairTestCases)
            .Concat(TwoPairsTestCases)
            .Concat(LowStraightTestCases)
            .Concat(HighStraightTestCases);

        private static Table GivenTable(Hand[] hands) =>
            new(hands);


    }
}