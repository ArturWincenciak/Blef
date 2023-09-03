using Blef.Modules.Games.Domain.Exceptions;
using Blef.Modules.Games.Domain.Model;

namespace Blef.Modules.Games.Domain.Services;

internal sealed class MoveOrderPolicy
{
    private readonly MoveSequence _sequence;
    private Move? _previousMove;

    public MoveOrderPolicy(MoveSequence sequence) =>
        _sequence = sequence ?? throw new ArgumentNullException(nameof(sequence));

    public void CheckMoveOrder(PlayerId movingPlayer)
    {
        if (CheckIfThatIsThePlayerMove(movingPlayer) == false)
            throw new ThatIsNotThisPlayerTurnNowException(movingPlayer);
    }

    public void CommitMove(PlayerId movingPlayer)
    {
        CheckMoveOrder(movingPlayer);
        _previousMove = _sequence.GetMove(movingPlayer);
    }

    private bool CheckIfThatIsThePlayerMove(PlayerId movingPlayer)
    {
        var isThatFirstMove = _previousMove is null;
        if (isThatFirstMove)
        {
            var firstMoveInSequence = _sequence.FirstMove;
            return IsPlayerInSequence(movingPlayer, firstMoveInSequence);
        }

        var lastMoveInSequence = _sequence.LastMove;
        var previousMoveWasLastInSequence = _previousMove!.Order == lastMoveInSequence.Order;
        if (previousMoveWasLastInSequence)
        {
            var firstMoveInSequence = _sequence.FirstMove;
            return IsPlayerInSequence(movingPlayer, firstMoveInSequence);
        }

        var nextOrder = _previousMove.Order.Next;
        var nextMoveInSequence = _sequence.GetMove(nextOrder);
        return IsPlayerInSequence(movingPlayer, nextMoveInSequence);
    }

    private static bool IsPlayerInSequence(PlayerId player, Move moveInSequence) =>
        moveInSequence.Player == player;

}