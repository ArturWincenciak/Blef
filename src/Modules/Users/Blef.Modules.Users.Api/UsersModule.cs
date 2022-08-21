using Blef.Modules.Users.Core;
using Blef.Shared.Abstractions.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Blef.Modules.Users.Api;

internal class UsersModule : IModule
{
    public const string BasePath = "users-module";
    public string Name => "Users";
    public string Path => BasePath;

    public void Register(IServiceCollection services) =>
        services.AddCore();

    public void Use(IApplicationBuilder app) { }
}