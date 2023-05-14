using Blef.Modules.Games.Domain.Model;
using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.Commands;

public sealed record StartFirstDeal(GameId GameId) : ICommand<StartFirstDeal.Result>
{
    public sealed record Result(int Number) : ICommandResult;
}