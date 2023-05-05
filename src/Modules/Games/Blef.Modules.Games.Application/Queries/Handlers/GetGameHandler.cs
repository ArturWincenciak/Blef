using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.Repositories;
using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Ids;
using Blef.Shared.Abstractions.Queries;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;

namespace Blef.Modules.Games.Application.Queries.Handlers;

[UsedImplicitly]
internal sealed class GetGameHandler : IQueryHandler<GetGame, GetGame.Result>
{
    private readonly IGameplaysRepository _gameplaysRepository;

    public GetGameHandler(IGameplaysRepository gameplaysRepository) =>
        _gameplaysRepository = gameplaysRepository;

    public async Task<GetGame.Result> Handle(GetGame query, CancellationToken cancellation)
    {
        var gameplay = _gameplaysRepository.Get(query.Game);
        var gameProjection = gameplay.GetGameProjection();
        var result = Map(gameProjection);
        return result;
    }

    private GetGame.Result Map(GameplayProjection.GameProjection gameProjection) =>
        new(Map(gameProjection.Status),
            Map(gameProjection.GamePlayers),
            Map(gameProjection.Deals),
            Map(gameProjection.Winner));

    private GetGame.GameStatus Map(GameplayProjection.GameStatus gameStatus) =>
        new(gameStatus.ToString());

    private static IEnumerable<GetGame.Player> Map(IEnumerable<(GamePlayer Player, int JoiningOrder)> gamePlayers) =>
        gamePlayers.Select(player => new GetGame.Player(
            player.Player.Id.Id, player.Player.Nick.Nick, player.JoiningOrder));

    private static IEnumerable<GetGame.Deal> Map(IEnumerable<(DealNumber Deal, GameplayProjection.DealStatus State, CheckingPlayer? CheckingPlayer, LooserPlayer? Looser)> deals) =>
        deals.Select(deal => new GetGame.Deal(
                Number: deal.Deal.Number,
                State: deal.State.ToString(),
                CheckingPlayerId: deal.CheckingPlayer?.Player.Id ?? Guid.Empty,
                LooserPlayerId: deal.Looser?.Player.Id ?? Guid.Empty));

    private static GetGame.Winner Map(GamePlayer? gamePlayer) =>
        new(gamePlayer?.Id.Id ?? Guid.Empty);
}