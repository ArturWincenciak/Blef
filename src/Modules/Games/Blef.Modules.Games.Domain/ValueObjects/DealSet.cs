namespace Blef.Modules.Games.Domain.ValueObjects;

internal sealed class DealSet
{
    public DealPlayersSet PlayersSet { get; }
    public MoveSequence MoveSequence { get; }

    public DealSet(DealPlayersSet playersSet, MoveSequence moveSequence)
    {
        if(playersSet is null)
            throw new ArgumentNullException(nameof(playersSet));

        if(moveSequence is null)
            throw new ArgumentNullException(nameof(moveSequence));

        if (NotAllPlayersPresent(playersSet, moveSequence))
            throw new ArgumentException("All players should be present in move sequence");

        PlayersSet = playersSet;
        MoveSequence = moveSequence;
    }

    private static bool NotAllPlayersPresent(DealPlayersSet playersSet, MoveSequence moveSequence) =>
        playersSet.Players
            .Select(player => player.PlayerId)
            .Except(moveSequence.Players)
            .Any();
}