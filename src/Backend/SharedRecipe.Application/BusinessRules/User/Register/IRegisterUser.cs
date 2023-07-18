using SharedRecipe.Reporting.Requests;
using SharedRecipe.Reporting.Responses;

namespace SharedRecipe.Application.BusinessRules.User.Register
{
    public interface IRegisterUser
    {
        public Task<UserResponseJson> Execute(UserRequestJson userRequestJson);
    }
}
