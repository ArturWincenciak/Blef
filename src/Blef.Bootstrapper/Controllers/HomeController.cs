using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Blef.Bootstrapper.Controllers;

[ApiController]
[Route("")]
public class HomeController : ControllerBase
{
    [HttpGet]
    public string Get()
    {
        return JsonSerializer.Serialize(new
        {
            Aplication = "Blef",
            Description = "Card game",
            Specification = "/swagger/index.html",
            Repository = "https://github.com/ArturWincenciak/Blef",
            DockerHub = "https://hub.docker.com/repository/docker/teovincent/blef",
            RequestTime = DateTime.UtcNow
        }, new JsonSerializerOptions {WriteIndented = true});
    }
}