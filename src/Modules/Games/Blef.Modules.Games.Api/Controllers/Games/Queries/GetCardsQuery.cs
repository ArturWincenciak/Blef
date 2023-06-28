﻿using System.ComponentModel.DataAnnotations;
using Blef.Modules.Games.Api.Controllers.Games.Commands.Bids.Validators;

namespace Blef.Modules.Games.Api.Controllers.Games.Queries;

public sealed record GetCardsQuery(
    [NotEmptyGuid] Guid GameId,
    [NotEmptyGuid] Guid PlayerId,
    [Range(minimum: 1, int.MaxValue)] int DealNumber)
{
    public const string ROUTE =
        $"{{{nameof(GameId)}:Guid}}/players/{{{nameof(PlayerId)}:Guid}}/deals/{{{nameof(DealNumber)}:int}}/cards";
}