namespace Blef.Modules.Games.Domain.ValueObjects;

internal sealed class MoveSequence
{
    public IEnumerable<Move> Moves { get; }

    public MoveSequence(IEnumerable<Move> moves)
    {
        if (moves is null)
            throw new ArgumentNullException(nameof(moves));

        if (moves.Count() < MIN_NUMBER_OF_PLAYERS)
            throw new ArgumentOutOfRangeException(nameof(moves),
                "Move sequence should have at least two players");

        if (moves.Count() > MAX_NUMBER_OF_PLAYERS)
            throw new ArgumentOutOfRangeException(nameof(moves),
                "Move sequence cannot contain more than four players");

        if (moves.Select(move => move.Player).Distinct().Count() != moves.Count())
            throw new ArgumentException("No player duplicates are allowed");

        if (moves.Select(move => move.Player).Distinct().Count() != moves.Count())
            throw new ArgumentException("No move duplicates are allowed");

        if (CheckIfMovesAreInOrder(moves) == false)
            throw new ArgumentException("Moves are not in order");

        Moves = moves;
    }

    private static bool CheckIfMovesAreInOrder(IEnumerable<Move> moves)
    {
        var orderedMoves = moves.OrderBy(move => move.Order);
        var expectedOrder = Order.First;
        foreach (var move in orderedMoves)
        {
            if (move.Order != expectedOrder)
                return false;

            if(expectedOrder != Order.Create(MAX_NUMBER_OF_PLAYERS))
                expectedOrder = expectedOrder.Next;
        }

        return true;
    }
}