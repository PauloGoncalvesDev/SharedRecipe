using Microsoft.AspNetCore.Mvc;
using SharedRecipe.Application.BusinessRules.User.Register;

namespace SharedRecipe.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get()
        {
            var usecases = new RegisterUser();
            await usecases.Execute(new Reporting.Requests.UserRequestJson { });

            return Ok();
        }
    }
}