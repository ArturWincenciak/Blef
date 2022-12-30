using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Blef.Shared.Abstractions.Modules;

public interface IModule
{
    string Name { get; }

    string Path { get; }

    void Register(IServiceCollection services);

    [SuppressMessage(category: "ReSharper", checkId: "UnusedParameter.Global")]
    void Use(IApplicationBuilder app);
}