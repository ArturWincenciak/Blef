using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Blef.Shared.Abstractions.Modules;

public interface IModule
{
    string Name { get; }

    string Path { get; }

    [UsedImplicitly]
    void Register(IServiceCollection services);

    [UsedImplicitly]
    void Use(IApplicationBuilder app);
}