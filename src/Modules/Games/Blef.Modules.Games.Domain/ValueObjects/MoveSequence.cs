namespace Blef.Modules.Games.Domain.ValueObjects;

// todo: validation
// todo: test
internal sealed record MoveSequence(IEnumerable<Move> Moves);