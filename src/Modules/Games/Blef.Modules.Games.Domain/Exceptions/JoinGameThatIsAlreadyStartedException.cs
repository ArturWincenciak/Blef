﻿using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Domain.Exceptions;

internal sealed class JoinGameThatIsAlreadyStartedException : BlefException
{
    public JoinGameThatIsAlreadyStartedException(Guid gameId, Guid playerId)
        : base(
            title: "Cannot join game that is already started",
            detail: $"Player '{playerId}' has tried to join game '{gameId}' that is already started",
            instance: $"/games/{gameId}")
    {
    }
}

internal sealed class JoinTournamentThatIsAlreadyStartedException : BlefException
{
    public JoinTournamentThatIsAlreadyStartedException(Guid tournamentsId, Guid playerId)
        : base(
            title: "Cannot join tournament that is already started",
            detail: $"Player '{playerId}' has tried to join game '{tournamentsId}' that is already started",
            instance: $"/tournaments/{tournamentsId}")
    {
    }
}