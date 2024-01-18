using Microsoft.AspNetCore.Mvc;

namespace UserTasks.Controllers;


[Route("api/[controller]")]
[ApiController]
public class UserTasks : ControllerBase
{
    //private readonly ILogger<WeatherForecastController> _logger;

    public UserTasks()
    {
    }

    [HttpGet]
    public string Get()
    {
        return "Hello Wolrd";
    }
}
