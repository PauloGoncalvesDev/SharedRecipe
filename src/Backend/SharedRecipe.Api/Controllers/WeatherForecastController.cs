using Microsoft.AspNetCore.Mvc;
using SharedRecipe.Application.BusinessRules.User.Register;
using SharedRecipe.Reporting.Responses;

namespace SharedRecipe.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get([FromServices] IRegisterUser registerUser)
        {
            UserResponseJson userResponseJson = await registerUser.Execute(new Reporting.Requests.UserRequestJson
            {
                Email = "gui@gmail.com",
                Name = "Paulin Guilherme",
                Password = "123456788",
                Phone = "88 9 9639-5192"
            });

            return Ok(userResponseJson);
        }
    }
}