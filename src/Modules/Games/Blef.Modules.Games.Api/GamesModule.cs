using Blef.Modules.Games.Infrastructure;
using Blef.Shared.Abstractions.Modules;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Blef.Modules.Games.Api;

[UsedImplicitly]
internal sealed class GamesModule : IModule
{
    public const string BASE_PATH = "games-module";
    public string Name => "Games";
    public string Path => BASE_PATH;

    public void Register(IServiceCollection services) =>
        services.AddInfrastructure();

    public void Use(IApplicationBuilder app)
    {
    }
}