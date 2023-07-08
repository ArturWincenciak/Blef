using System.Diagnostics.CodeAnalysis;

namespace Blef.Modules.Games.Api.Tests.Scenarios.ValueObjects;

internal enum Suit
{
    Clubs = 1,
    Diamonds = 2,
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    Hearts = 4,
    Spades = 8,
    NotValidValue = 16
}
