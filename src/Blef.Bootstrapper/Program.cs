using Blef.Shared.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure();

var app = builder.Build();

app.UseInfrastructure();

app.Run();