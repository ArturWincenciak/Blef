using Blef.Modules.Games.Api.Tests.Scenarios.Services;
using Blef.Modules.Games.Domain.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Blef.Modules.Games.Api.Tests.Core;

internal sealed class BlefApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.Replace(
                ServiceDescriptor.Singleton<IDeckFactory>(
                    _ => new DeckFactoryMock()));
        });

        builder.UseEnvironment("test");
    }
}