namespace Blef.Modules.Games.Domain.Entities;

// todo: validation
internal sealed record MoveSequence(IEnumerable<Move> Moves);