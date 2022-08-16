using Microsoft.AspNetCore.Mvc;
using WeatherForecastApi.Data;
using WeatherForecastApi.Models;

namespace WeatherForecastApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly WeatherForecastContext _context;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            WeatherForecastContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get()
        {
            return Ok(_context.WeatherForecasts);
        }
    }
}