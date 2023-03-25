namespace Blef.Modules.Games.Api.Tests.Core.ValueObjects;

internal class Deal
{
    internal int Number { get; }

    public Deal(int number)
    {
        if (number < 1) // todo: better exception
            throw new Exception("Invalid deal number");

        Number = number;
    }
}