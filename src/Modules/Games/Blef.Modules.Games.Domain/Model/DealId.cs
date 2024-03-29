﻿namespace Blef.Modules.Games.Domain.Model;

internal sealed record DealId(GameId Game, DealNumber Deal)
{
    public GameId Game { get; } =
        Game ?? throw new ArgumentNullException(nameof(Game));

    public DealNumber Deal { get; } =
        Deal ?? throw new ArgumentNullException(nameof(Deal));
}