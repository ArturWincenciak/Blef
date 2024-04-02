using Blef.Modules.Games.Application.Repositories;
using Blef.Modules.Games.Domain.Model;

namespace Blef.Bootstrapper.Tests.Mocks;

internal sealed class ThrowGameplayRepository : IGameplaysRepository
{
    public Task Add(Gameplay gameplay) =>
        Task.CompletedTask;

    public Task<Gameplay?> Get(GameId gameId) =>
        throw new("Test exception on get gameplay");
}