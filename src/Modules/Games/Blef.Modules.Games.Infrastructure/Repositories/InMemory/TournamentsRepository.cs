using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.Repositories;

namespace Blef.Modules.Games.Infrastructure.Repositories.InMemory;

internal sealed class TournamentsRepository : ITournamentsRepository
{
    private readonly Dictionary<Guid, Tournament> _tournaments = new();

    public void Add(Tournament tournament) =>
        _tournaments.Add(tournament.Id, tournament);

    public Tournament Get(Guid tournamentId) =>
        _tournaments[tournamentId];
}