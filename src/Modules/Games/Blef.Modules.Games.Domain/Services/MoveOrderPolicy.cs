using Blef.Modules.Games.Domain.Exceptions;
using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.Services;

// todo: test
internal sealed class MoveOrderPolicy
{
    private Move _previousMove;
    private readonly MoveSequence _sequence;

    public MoveOrderPolicy(MoveSequence sequence) =>
        _sequence = sequence;

    public void Move(PlayerId movingPlayer)
    {
        if (CheckIfThatIsThePlayerMove(movingPlayer) == false)
            throw new ThatIsNotThisPlayerTurnNowException(movingPlayer);

        _previousMove = _sequence.Moves.Single(move => move.Player == movingPlayer);
    }

    private bool CheckIfThatIsThePlayerMove(PlayerId movingPlayer)
    {
        var isThatFirstMove = _previousMove is null;
        if (isThatFirstMove)
        {
            var firstPlayerInSequence = _sequence.Moves.Single(player => player.Order == Order.First);
            return firstPlayerInSequence.Player == movingPlayer;
        }

        var lastInSequence = Order.Create(_sequence.Moves.Count());
        var isPreviousMoveLastInSequence = _previousMove.Order == lastInSequence;
        if (isPreviousMoveLastInSequence)
        {
            var firstPlayerInSequence = _sequence.Moves.Single(player => player.Order == Order.First);
            return firstPlayerInSequence.Player == movingPlayer;
        }

        var nextMove = _previousMove.Order.Next;
        var nextMoveInSequence = _sequence.Moves.Single(player => player.Order == nextMove);
        return nextMoveInSequence.Player == movingPlayer;
    }
}