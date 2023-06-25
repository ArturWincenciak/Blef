using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.Commands;

public sealed record Check(Guid GameId, Guid PlayerId) : ICommand<Check.Result>
{
    public sealed record Result(int DealNumber) : ICommandResult;
};
