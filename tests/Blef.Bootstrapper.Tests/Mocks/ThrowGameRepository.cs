﻿using Blef.Modules.Games.Application.Repositories;
using Blef.Modules.Games.Domain.Model;

namespace Blef.Bootstrapper.Tests.Mocks;

internal sealed class ThrowGameRepository : IGamesRepository
{
    public Task Add(Game game) =>
        throw new Exception("Test exception on add new game");

    public Task<Game?> Get(GameId gameId) =>
        throw new Exception("Test exception on get game");
}