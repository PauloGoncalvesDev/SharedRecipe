using FluentValidation;
using SharedRecipe.Reporting.Requests;

namespace SharedRecipe.Application.Validators.User
{
    public class ValidateChangePassword : AbstractValidator<ChangePasswordRequestJson>
    {
        public ValidateChangePassword()
        {
            ValidatePassword();
        }

        private void ValidatePassword()
        {
            RuleFor(c => c.NewPassword).SetValidator(new ValidatePasswordDefault());
        }
    }
}
