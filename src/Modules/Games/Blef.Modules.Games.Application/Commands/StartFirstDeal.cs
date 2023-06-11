using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.Commands;

public sealed record StartFirstDeal(Guid GameId) : ICommand<StartFirstDeal.Result>
{
    public sealed record Result(int Number) : ICommandResult;
}