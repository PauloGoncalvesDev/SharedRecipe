using FluentAssertions;
using SharedRecipe.Application.BusinessRules.User.Login;
using SharedRecipe.Application.Services.Cryptography;
using SharedRecipe.Application.Services.Token;
using SharedRecipe.Domain.Repositories.User;
using SharedRecipe.Exceptions;
using SharedRecipe.Exceptions.ExceptionsBase;
using SharedRecipe.Reporting.Requests;
using SharedRecipe.Reporting.Responses;
using TestUtility.Cryptography;
using TestUtility.Entities;
using TestUtility.Repositories;
using TestUtility.Token;
using Xunit;

namespace UseCases.Test.User.Login
{
    public class UserLoginBusinessRuleTest
    {
        [Fact]
        public async Task Validate_Business_Rule_Success()
        {
            (SharedRecipe.Domain.Entities.User user, string password) = UserBuilder.GenerateUser();

            UserLogin userLogin = CreateUserLogin(user);

            UserLoginResponseJson userLoginResponseJson = await userLogin.Execute(
                new UserLoginRequestJson
                {
                    Email = user.Email,
                    Password = password
                });

            userLoginResponseJson.Should().NotBeNull();
            userLoginResponseJson.Token.Should().NotBeNull();
            userLoginResponseJson.Name.Should().NotBeNull().And.Be(user.Name);
            userLoginResponseJson.Message.Should().NotBeNull().And.BeSameAs(APIMSG.LOGIN_COMPLETED);
            userLoginResponseJson.Success.Should().BeTrue();
        }

        [Fact]
        public async Task Validate_Invalid_Email_Error()
        {
            (SharedRecipe.Domain.Entities.User user, string password) = UserBuilder.GenerateUser();

            UserLogin userLogin = CreateUserLogin(user);

            Func<Task> userLoginException = async () =>
            {
                await userLogin.Execute(
                new UserLoginRequestJson
                {
                    Email = "emailerrado@errado.com",
                    Password = password
                });
            };

            await userLoginException.Should().ThrowAsync<InvalidLoginException>()
                .Where(exception => exception.Message.Equals(APIMSG.LOGIN_ERROR));
        }

        [Fact]
        public async Task Validate_Invalid_Password_Error()
        {
            (SharedRecipe.Domain.Entities.User user, string password) = UserBuilder.GenerateUser();

            UserLogin userLogin = CreateUserLogin(user);

            Func<Task> userLoginException = async () =>
            {
                await userLogin.Execute(
                new UserLoginRequestJson
                {
                    Email = user.Email,
                    Password = "invalidpassword"
                });
            };

            await userLoginException.Should().ThrowAsync<InvalidLoginException>()
                .Where(exception => exception.Message.Equals(APIMSG.LOGIN_ERROR));
        }

        [Fact]
        public async Task Validate_Invalid_Email_And_Password_Error()
        {
            (SharedRecipe.Domain.Entities.User user, string password) = UserBuilder.GenerateUser();

            UserLogin userLogin = CreateUserLogin(user);

            Func<Task> userLoginException = async () =>
            {
                await userLogin.Execute(
                new UserLoginRequestJson
                {
                    Email = "emailerrado@errado.com",
                    Password = "invalidpassword"
                });
            };

            await userLoginException.Should().ThrowAsync<InvalidLoginException>()
                .Where(exception => exception.Message.Equals(APIMSG.LOGIN_ERROR));
        }

        private UserLogin CreateUserLogin(SharedRecipe.Domain.Entities.User user)
        {
            IUserReadOnlyRepository userReadOnlyRepository = UserReadOnlyRepositoryBuilder.CreateInstance().GetUserLogin(user).UserReadOnlyRepository();

            PasswordEncryption passwordEncryption = PasswordEncryptionBuilder.CreateInstance();

            TokenController tokenController = TokenControllerBuilder.CreateInstance();

            return new UserLogin(userReadOnlyRepository, passwordEncryption, tokenController);
        }
    }
}
