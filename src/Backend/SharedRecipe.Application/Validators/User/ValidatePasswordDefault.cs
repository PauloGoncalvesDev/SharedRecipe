using SharedRecipe.Exceptions;
using FluentValidation;

namespace SharedRecipe.Application.Validators.User
{
    public class ValidatePasswordDefault : AbstractValidator<string>
    {
        public ValidatePasswordDefault()
        {
            RuleFor(password => password).NotEmpty().WithMessage(string.Format(APIMSG.EMPTY_PASSWORD));
            When(password => !string.IsNullOrEmpty(password), () =>
            {
                RuleFor(password => password.Length).GreaterThanOrEqualTo(6).WithMessage(APIMSG.LENGTH_PASSWORD);
            });
        }
    }
}
