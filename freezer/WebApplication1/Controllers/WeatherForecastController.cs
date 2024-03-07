using freezer.Logic;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
    private readonly IFreezerLogic _logic;

        public WeatherForecastController(IFreezerLogic logic)
        {
            _logic = logic;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
