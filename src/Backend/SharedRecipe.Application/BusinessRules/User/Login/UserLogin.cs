using SharedRecipe.Application.Services.Cryptography;
using SharedRecipe.Application.Services.Token;
using SharedRecipe.Domain.Repositories;
using SharedRecipe.Exceptions.ExceptionsBase;
using SharedRecipe.Reporting.Requests;
using SharedRecipe.Reporting.Responses;

namespace SharedRecipe.Application.BusinessRules.User.Login
{
    public class UserLogin : IUserLogin
    {
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;

        private readonly PasswordEncryption _passwordEncryption;

        private readonly TokenController _tokenController;

        public UserLogin(IUserReadOnlyRepository userReadOnlyRepository, PasswordEncryption passwordEncryption, TokenController tokenController)
        {
            _userReadOnlyRepository = userReadOnlyRepository;
            _passwordEncryption = passwordEncryption;
            _tokenController = tokenController;
        }

        public async Task<UserLoginResponseJson> Execute(UserLoginRequestJson userLoginRequest)
        {
            Domain.Entities.User user = await _userReadOnlyRepository.GetUserLogin(userLoginRequest.Email, _passwordEncryption.Encrypt(userLoginRequest.Password));

            if (user == null)
                throw new InvalidLoginException();

            return new UserLoginResponseJson
            {
                Name = user.Name,
                Message = "deu certo",
                Success = true,
                Token = _tokenController.GenerateTokenJwt(user.Email)
            };
        }
    }
}
