using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.Commands;

public sealed record StartTournament(Guid TournamentId) : ICommand;