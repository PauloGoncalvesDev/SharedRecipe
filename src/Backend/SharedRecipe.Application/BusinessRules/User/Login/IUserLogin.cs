using SharedRecipe.Reporting.Requests;
using SharedRecipe.Reporting.Responses;

namespace SharedRecipe.Application.BusinessRules.User.Login
{
    public interface IUserLogin
    {
        public Task<UserLoginResponseJson> Execute(UserLoginRequestJson userLoginRequest);
    }
}
