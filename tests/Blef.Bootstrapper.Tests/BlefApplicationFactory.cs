using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Blef.Bootstrapper.Tests;

internal sealed class BlefApplicationFactory : WebApplicationFactory<Program>
{
    private readonly string _environment;

    public BlefApplicationFactory(string environment = Environments.DEVELOPMENT) =>
        _environment = environment;

    protected override void ConfigureWebHost(IWebHostBuilder builder) =>
        builder.UseEnvironment(_environment);
}