namespace Blef.Modules.Games.Api.Tests.Core.ValueObjects;

internal sealed record DealNumber
{
    public int Number { get; }

    public DealNumber(int number)
    {
        if (number < 1) // todo: better exception
            throw new Exception("Invalid deal number");

        Number = number;
    }
}