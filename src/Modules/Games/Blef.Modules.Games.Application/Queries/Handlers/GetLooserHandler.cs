using Blef.Modules.Games.Application.Queries;
using Blef.Modules.Games.Domain;
using Blef.Shared.Abstractions.Queries;

internal sealed class GetLooserHandler : IQueryHandler<GetLooser, GetLooser.Result>
{
    private readonly Games _games;

    public GetLooserHandler(Games games)
    {
        _games = games;
    }

    public Task<GetLooser.Result> Handle(GetLooser query)
    {
        var game = _games.GetExistingGame(query.GameId);
        var looser = game.GetLooser();
        return Task.FromResult(new GetLooser.Result(looser));
    }
}