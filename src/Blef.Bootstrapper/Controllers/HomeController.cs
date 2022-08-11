using Microsoft.AspNetCore.Mvc;

namespace Blef.Bootstrapper.Controllers;

[ApiController]
[Route("")]
public class HomeController : ControllerBase
{
    [HttpGet]
    public object Get()
    {
        return new
        {
            Aplication = "Blef",
            ApiSpecification = "/swagger/index.html",
            Repository = "https://github.com/ArturWincenciak/Blef",
            RequestTime = DateTime.UtcNow,
        };
    }
}