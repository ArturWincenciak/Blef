using Blef.Bootstrapper;
using Blef.Shared.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

Directory.EnumerateFiles(
        path: builder.Environment.ContentRootPath,
        searchPattern: "*.module.json",
        SearchOption.AllDirectories)
    .ToList()
    .ForEach(config => builder.Configuration.AddJsonFile(config));

Directory.EnumerateFiles(
        path: builder.Environment.ContentRootPath,
        searchPattern: $"*.module.{builder.Environment.EnvironmentName}.json",
        SearchOption.AllDirectories)
    .ToList()
    .ForEach(config => builder.Configuration.AddJsonFile(config));

var assemblies = ModuleLoader.LoadAssemblies(builder.Configuration);
var modules = ModuleLoader.LoadModules(assemblies).ToList();

builder.Services.AddInfrastructure();
modules.ForEach(module => module.Register(builder.Services));

var app = builder.Build();
app.UseInfrastructure();
modules.ForEach(module => module.Use(app));

app.Run();
