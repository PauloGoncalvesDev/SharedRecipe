using FluentValidation;
using SharedRecipe.Reporting.Requests;
using SharedRecipe.Exceptions;
using System.Text.RegularExpressions;

namespace SharedRecipe.Application.Validators.User
{
    public class ValidateUser : AbstractValidator<UserRequestJson>
    {
        public ValidateUser()
        {
            ValidateName();

            ValidateEmail();

            ValidatePhone();

            ValidatePassword();
        }

        private void ValidateName()
        {
            RuleFor(r => r.Name).NotEmpty().WithMessage(string.Format(APIMSG.EMPTY_NAME));
        }

        private void ValidateEmail()
        {
            RuleFor(r => r.Email).NotEmpty().WithMessage(string.Format(APIMSG.EMPTY_EMAIL));
            When(r => !string.IsNullOrEmpty(r.Email), () =>
            {
                RuleFor(r => r.Email).EmailAddress().WithMessage(APIMSG.INVALID_EMAIL);
            });
        }

        private void ValidatePhone()
        {
            RuleFor(r => r.Phone).NotEmpty().WithMessage(string.Format(APIMSG.EMPTY_PHONE));
            When(r => !string.IsNullOrEmpty(r.Phone), () =>
            {
                RuleFor(r => r.Phone).Custom((phone, context) =>
                {
                    string regexPhone = "[0-9]{2} [1-9]{1} [0-9]{4}-[0-9]{4}";
                    bool isMatch = Regex.IsMatch(phone, regexPhone);

                    if (!isMatch)
                        context.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(phone), APIMSG.FORMAT_PHONE));
                });
            });
        }

        private void ValidatePassword()
        {
            RuleFor(r => r.Password).NotEmpty().WithMessage(string.Format(APIMSG.EMPTY_PASSWORD));
            When(r => !string.IsNullOrEmpty(r.Password), () =>
            {
                RuleFor(r => r.Password.Length).GreaterThanOrEqualTo(6).WithMessage(APIMSG.LENGTH_PASSWORD);
            });
        }
    }
}
