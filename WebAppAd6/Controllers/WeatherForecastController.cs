using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebAppAd6.CustomAuthorize;
using System.DirectoryServices.Protocols;

namespace WebAppAd6.Controllers {
    //[Authorize]
    [ApiController]
    [Route("WeatherForecast")]
    public class WeatherForecastController : ControllerBase {
        private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IEnumerable<string> _availableUsers;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IOptions<AppSettings> options
        ) {
            _logger = logger;
            _availableUsers = options.Value.AvailableUsers;
        }

        [CustomAuthorize("MILF\\Administrator")]
        [HttpGet("milf-admin")]
        public ActionResult<IEnumerable<WeatherForecast>> GetAdmin() {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [CustomAuthorize("DESKTOP-99FLTJP\\Nikolai")]
        [HttpGet("desktop-nikolay")]
        public ActionResult<IEnumerable<WeatherForecast>> GetDesktop() {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [CustomAuthorize("MILF\\NikolaiS")]
        [HttpGet("milf-nikolaiS")]
        public ActionResult<IEnumerable<WeatherForecast>> GetNikolaiS() {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}