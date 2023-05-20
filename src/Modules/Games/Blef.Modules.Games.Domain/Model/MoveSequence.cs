namespace Blef.Modules.Games.Domain.Model;

internal sealed class MoveSequence
{
    private readonly IReadOnlyCollection<Move> _moves;

    public IReadOnlyCollection<PlayerId> Players => _moves
        .Select(move => move.Player)
        .ToArray();

    public Move FirstMove => _moves
        .Single(move => move.Order == Order.First);

    public Move LastMove => _moves
        .Single(move => move.Order == Order.Create(_moves.Count));

    public MoveSequence(IReadOnlyCollection<Move> moves)
    {
        if (moves is null)
            throw new ArgumentNullException(nameof(moves));

        if (moves.Count < MIN_NUMBER_OF_PLAYERS)
            throw new ArgumentOutOfRangeException(paramName: nameof(moves), actualValue: moves.Count,
                message: $"Move sequence should have at least {MIN_NUMBER_OF_PLAYERS} players");

        if (moves.Count > MAX_NUMBER_OF_PLAYERS)
            throw new ArgumentOutOfRangeException(paramName: nameof(moves), actualValue: moves.Count,
                message: $"Move sequence cannot contain more than {MAX_NUMBER_OF_PLAYERS} players");

        if (AreAllPlayersUnique(moves) == false)
            throw new ArgumentException("No player duplicates are allowed");

        if (AreAllMoveUnique(moves) == false)
            throw new ArgumentException("No move duplicates are allowed");

        if (CheckIfMovesAreInOrder(moves) == false)
            throw new ArgumentException("Moves are not in order");

        _moves = moves;
    }

    public Move GetMove(PlayerId movingPlayer) =>
        _moves.Single(move => move.Player == movingPlayer);

    public Move GetMove(Order order) =>
        _moves.Single(move => move.Order == order);

    private static bool AreAllMoveUnique(IReadOnlyCollection<Move> moves) =>
        moves.Select(move => move.Player).Distinct().Count() == moves.Count;

    private static bool AreAllPlayersUnique(IReadOnlyCollection<Move> moves) =>
        moves.Select(move => move.Player).Distinct().Count() == moves.Count;

    private static bool CheckIfMovesAreInOrder(IReadOnlyCollection<Move> moves)
    {
        var orderedMoves = moves.OrderBy(move => move.Order);
        var expectedOrder = Order.First;
        foreach (var move in orderedMoves)
        {
            if (move.Order != expectedOrder)
                return false;

            if (expectedOrder != Order.Create(MAX_NUMBER_OF_PLAYERS))
                expectedOrder = expectedOrder.Next;
        }

        return true;
    }
}