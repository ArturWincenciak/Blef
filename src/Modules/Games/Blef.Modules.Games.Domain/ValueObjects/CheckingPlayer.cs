﻿using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.ValueObjects;

internal sealed record CheckingPlayer(PlayerId Player)
{
    public PlayerId Player { get; } = Player ?? throw new ArgumentNullException(nameof(Player));
}