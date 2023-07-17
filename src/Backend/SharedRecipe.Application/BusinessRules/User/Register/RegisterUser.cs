using FluentValidation.Results;
using SharedRecipe.Application.Validators.User;
using SharedRecipe.Exceptions.ExceptionsBase;
using SharedRecipe.Reporting.Requests;

namespace SharedRecipe.Application.BusinessRules.User.Register
{
    public class RegisterUser
    {
        public async Task Execute(UserRequestJson userRequestJson)
        {
            ValidateUser(userRequestJson);
        }

        private void ValidateUser(UserRequestJson userRequestJson)
        {
            ValidationResult validationResult = new ValidateUser().Validate(userRequestJson);

            if (!validationResult.IsValid)
            {
                List<string> errorMessage = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ValidationException(errorMessage);
            }
        }
    }
}
