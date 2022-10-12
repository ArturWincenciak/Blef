using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.Commands;

public sealed record JoinTournament(Guid TournamentId, Guid PlayerId) : ICommand;