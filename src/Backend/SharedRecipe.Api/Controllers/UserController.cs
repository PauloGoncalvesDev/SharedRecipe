using Microsoft.AspNetCore.Mvc;
using SharedRecipe.Application.BusinessRules.User.Register;
using SharedRecipe.Reporting.Requests;
using SharedRecipe.Reporting.Responses;

namespace SharedRecipe.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(UserResponseJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> RegisterUser([FromServices] IRegisterUser registerUser, [FromBody] UserRequestJson userRequestJson)
        {
            UserResponseJson userResponseJson = await registerUser.Execute(userRequestJson);

            return Created(string.Empty, userResponseJson);    
        }
    }
}