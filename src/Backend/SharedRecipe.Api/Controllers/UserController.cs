using Microsoft.AspNetCore.Mvc;
using SharedRecipe.Api.Filters;
using SharedRecipe.Application.BusinessRules.User.ChangePassword;
using SharedRecipe.Application.BusinessRules.User.Register;
using SharedRecipe.Reporting.Requests;
using SharedRecipe.Reporting.Responses;

namespace SharedRecipe.Api.Controllers
{
    public class UserController : SharedRecipeControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(UserResponseJson), StatusCodes.Status201Created)]
        public async Task<IActionResult> RegisterUser([FromServices] IRegisterUser registerUser, [FromBody] UserRequestJson userRequestJson)
        {
            UserResponseJson userResponseJson = await registerUser.Execute(userRequestJson);

            return Created(string.Empty, userResponseJson);    
        }

        [HttpPut]
        [Route("ChangePassword")]
        [ProducesResponseType(typeof(ChangePasswordResponseJson), StatusCodes.Status200OK)]
        [ServiceFilter(typeof(UserAuthorization))]
        public async Task<IActionResult> ChangePassword([FromServices] IChangePassword changePassword, [FromBody] ChangePasswordRequestJson changePasswordRequestJson)
        {
            ChangePasswordResponseJson changePasswordResponse = await changePassword.Execute(changePasswordRequestJson);

            return Ok(changePasswordResponse);
        }
    }
}