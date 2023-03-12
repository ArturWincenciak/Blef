using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.Commands;

public sealed record NewDeal(Guid GameId) : ICommand<NewDeal.Result>
{
    public sealed record Result(int DealId) : ICommandResult;
}