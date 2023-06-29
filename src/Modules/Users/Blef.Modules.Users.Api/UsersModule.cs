using Blef.Modules.Users.Core;
using Blef.Shared.Abstractions.Modules;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Blef.Modules.Users.Api;

[UsedImplicitly]
internal sealed class UsersModule : IModule
{
    public const string BASE_PATH = "users-module";
    public string Name => "Users";
    public string Path => BASE_PATH;

    public void Register(IServiceCollection services) =>
        services.AddCore();

    public void Use(IApplicationBuilder app)
    {
    }
}
