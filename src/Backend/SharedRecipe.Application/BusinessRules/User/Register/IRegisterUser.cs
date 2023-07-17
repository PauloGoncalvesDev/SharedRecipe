using SharedRecipe.Reporting.Requests;

namespace SharedRecipe.Application.BusinessRules.User.Register
{
    public interface IRegisterUser
    {
        public Task Execute(UserRequestJson userRequestJson);
    }
}
