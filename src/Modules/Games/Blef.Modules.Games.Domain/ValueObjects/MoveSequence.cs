namespace Blef.Modules.Games.Domain.ValueObjects;

// todo: validation
internal sealed record MoveSequence(IEnumerable<Move> Moves);