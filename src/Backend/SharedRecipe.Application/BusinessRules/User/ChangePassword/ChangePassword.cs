using FluentValidation.Results;
using SharedRecipe.Application.Services.Cryptography;
using SharedRecipe.Application.Services.LoggedUser;
using SharedRecipe.Application.Validators.User;
using SharedRecipe.Domain.Repositories;
using SharedRecipe.Domain.Repositories.User;
using SharedRecipe.Exceptions;
using SharedRecipe.Exceptions.ExceptionsBase;
using SharedRecipe.Reporting.Requests;
using SharedRecipe.Reporting.Responses;

namespace SharedRecipe.Application.BusinessRules.User.ChangePassword
{
    public class ChangePassword : IChangePassword
    {
        private readonly IUserUpdateOnlyRepository _userUpdateOnlyRepository;

        private readonly ILoggedUser _loggedUser;

        private readonly PasswordEncryption _passwordEncryption;

        private readonly IWorkUnit _workUnit;

        public ChangePassword(IUserUpdateOnlyRepository userUpdateOnlyRepository, ILoggedUser loggedUser, PasswordEncryption passwordEncryption, IWorkUnit workUnit)
        {
            _userUpdateOnlyRepository = userUpdateOnlyRepository;
            _loggedUser = loggedUser;   
            _passwordEncryption = passwordEncryption;
            _workUnit = workUnit;

        }

        public async Task<ChangePasswordResponseJson> Execute(ChangePasswordRequestJson changePasswordRequestJson)
        {
            Domain.Entities.User loggedUser = await _loggedUser.GetLoggedUser();

            Domain.Entities.User user = await _userUpdateOnlyRepository.GetUserById(loggedUser.Id);

            ValidateChangePassword(changePasswordRequestJson, user);

            user.Password = _passwordEncryption.Encrypt(changePasswordRequestJson.NewPassword);

            _userUpdateOnlyRepository.ChangePassword(user);

            await _workUnit.Commit();

            return new ChangePasswordResponseJson
            {
                Message = APIMSG.PASSWORD_CHANGED,
                Success = true
            };
        }

        private void ValidateChangePassword(ChangePasswordRequestJson changePasswordRequestJson, Domain.Entities.User user)
        {
            ValidationResult validationResult = new ValidateChangePassword().Validate(changePasswordRequestJson);

            if (!_passwordEncryption.Encrypt(changePasswordRequestJson.OldPassword).Equals(user.Password))
                validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(changePasswordRequestJson.OldPassword, APIMSG.INVALID_PASSWORD));

            if (!validationResult.IsValid)
            {
                List<string> errorMessage = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ValidationException(errorMessage);
            }
        }
    }
}
