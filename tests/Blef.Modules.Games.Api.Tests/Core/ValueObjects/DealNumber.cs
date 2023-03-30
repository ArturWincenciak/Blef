namespace Blef.Modules.Games.Api.Tests.Core.ValueObjects;

internal sealed record DealNumber
{
    public int Number { get; }

    public DealNumber(int number)
    {
        if (number < 1) // todo: exception
            throw new Exception("TBD");

        Number = number;
    }
}