using Blef.Modules.Users.Api;
using Blef.Shared.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddInfrastructure();
services.AddUsers();

var app = builder.Build();

app.UseInfrastructure();

app.Run();