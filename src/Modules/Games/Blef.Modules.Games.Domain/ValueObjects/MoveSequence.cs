﻿using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.ValueObjects;

internal sealed class MoveSequence
{
    private readonly IEnumerable<Move> _moves;

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

        _moves = moves;
    }

    public IEnumerable<PlayerId> Players => _moves.Select(move => move.Player);
    public Move FirstMove => _moves.Single(move => move.Order == Order.First);
    public Move LastMove => _moves.Single(move => move.Order == Order.Create(_moves.Count()));

    public Move GetMove(PlayerId movingPlayer) =>
        _moves.Single(move => move.Player == movingPlayer);

    public Move GetMove(Order order) =>
        _moves.Single(move => move.Order == order);

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