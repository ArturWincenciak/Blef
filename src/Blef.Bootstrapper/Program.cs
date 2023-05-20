using System.Runtime.CompilerServices;
using Blef.Shared.Abstractions.Modules;
using Blef.Shared.Infrastructure.Extensions;
using static System.Activator;
using static System.IO.Directory;
using static Blef.Bootstrapper.ModuleLoader;

[assembly: InternalsVisibleTo(assemblyName: "Blef.Modules.Games.Api.Tests")]

var builder = WebApplication.CreateBuilder(args);

EnumerateFiles(
        builder.Environment.ContentRootPath,
        searchPattern: "*.module.json",
        SearchOption.AllDirectories)
    .ToList()
    .ForEach(config => builder.Configuration.AddJsonFile(config));

EnumerateFiles(
        builder.Environment.ContentRootPath,
        searchPattern: $"*.module.{builder.Environment.EnvironmentName}.json",
        SearchOption.AllDirectories)
    .ToList()
    .ForEach(config => builder.Configuration.AddJsonFile(config));

var assemblies = LoadAssemblies(builder.Configuration);

var modules = assemblies
    .SelectMany(assembly => assembly.GetTypes())
    .Where(type => typeof(IModule).IsAssignableFrom(type) && false == type.IsInterface)
    .OrderBy(type => type.Name)
    .Select(CreateInstance)
    .Cast<IModule>()
    .ToList();

builder.Services.AddInfrastructure(builder.Configuration, modules, assemblies);
modules.ForEach(module => module.Register(builder.Services));

var app = builder.Build();
app.UseInfrastructure();
modules.ForEach(module => module.Use(app));

app.Run();