using Blef.Shared.Infrastructure;
using Blef.Bootstrapper;

var builder = WebApplication.CreateBuilder(args);

var assemblies = ModuleLoader.LoadAssemblies(builder.Configuration);
var modules = ModuleLoader.LoadModules(assemblies);

builder.Services.AddInfrastructure();

foreach (var module in modules)
    module.Register(builder.Services);

var app = builder.Build();

app.UseInfrastructure();

foreach (var module in modules)
    module.Use(app);

app.Run();