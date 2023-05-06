using Blef.Modules.Games.Domain.Model;
using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.Commands;

public sealed record NewDeal(GameId GameId) : ICommand<NewDeal.Result>
{
    public sealed record Result(int Number) : ICommandResult;
}