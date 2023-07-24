using SharedRecipe.Reporting.Requests;
using SharedRecipe.Reporting.Responses;

namespace SharedRecipe.Application.BusinessRules.User.ChangePassword
{
    public interface IChangePassword
    {
        public Task<ChangePasswordResponseJson> Execute(ChangePasswordRequestJson changePasswordRequestJson);
    }
}
