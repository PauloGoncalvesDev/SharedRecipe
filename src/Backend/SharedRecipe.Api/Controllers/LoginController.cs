using Microsoft.AspNetCore.Mvc;
using SharedRecipe.Application.BusinessRules.User.Login;
using SharedRecipe.Reporting.Requests;
using SharedRecipe.Reporting.Responses;

namespace SharedRecipe.Api.Controllers
{
    public class LoginController : SharedRecipeControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(UserLoginResponseJson), StatusCodes.Status200OK)]
        public async Task<IActionResult> UserLogin([FromServices] IUserLogin userLogin, [FromBody] UserLoginRequestJson userLoginRequestJson)
        {
            UserLoginResponseJson userLoginResponseJson = await userLogin.Execute(userLoginRequestJson);

            return Ok(userLoginResponseJson);
        }
    }
}
