using Blef.Bootstrapper.Tests.Mocks;
using Blef.Modules.Games.Application.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Blef.Bootstrapper.Tests.Core;

internal sealed class BlefApplicationFactory : WebApplicationFactory<Program>
{
    private readonly string _environment;
    private bool _throwGameplayRepository;
    private bool _throwGameRepository;

    public BlefApplicationFactory(string environment = Environments.DEVELOPMENT) =>
        _environment = environment;

    public BlefApplicationFactory WithThrowGamesRepository()
    {
        _throwGameRepository = true;
        return this;
    }

    public BlefApplicationFactory WithThrowGameplayRepository()
    {
        _throwGameplayRepository = true;
        return this;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder) =>
        builder
            .UseEnvironment(_environment)
            .ConfigureServices(services =>
            {
                if (_throwGameRepository)
                    services.AddScoped<IGamesRepository, ThrowGameRepository>();

                if (_throwGameplayRepository)
                    services.AddScoped<IGameplaysRepository, ThrowGameplayRepository>();
            });
}