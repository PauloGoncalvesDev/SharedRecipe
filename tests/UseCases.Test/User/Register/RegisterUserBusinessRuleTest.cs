using AutoMapper;
using FluentAssertions;
using SharedRecipe.Application.BusinessRules.User.Register;
using SharedRecipe.Application.Services.Cryptography;
using SharedRecipe.Application.Services.Token;
using SharedRecipe.Domain.Repositories;
using SharedRecipe.Domain.Repositories.User;
using SharedRecipe.Exceptions;
using SharedRecipe.Exceptions.ExceptionsBase;
using SharedRecipe.Reporting.Requests;
using SharedRecipe.Reporting.Responses;
using TestUtility.Automapper;
using TestUtility.Cryptography;
using TestUtility.Repositories;
using TestUtility.Requests;
using TestUtility.Token;
using Xunit;

namespace UseCases.Test.User.Register
{
    public class RegisterUserBusinessRuleTest
    {
        [Fact]
        public async Task Validate_Business_Rule_Success()
        {
            UserRequestJson userRequestJson = RegisterUserRequestBuilder.GenerateUserRequest();

            RegisterUser registerUser = CreateRegisterUser();

            UserResponseJson userResponseJson = await registerUser.Execute(userRequestJson);

            userResponseJson.Should().NotBeNull();
            userResponseJson.Token.Should().NotBeNullOrWhiteSpace();
            userResponseJson.Message.Should().NotBeNullOrWhiteSpace().And.BeSameAs(APIMSG.USER_CREATED);
            userResponseJson.Success.Should().BeTrue();
        }

        [Fact]
        public async Task Validate_Existing_Email_Error()
        {
            UserRequestJson userRequestJson = RegisterUserRequestBuilder.GenerateUserRequest();

            RegisterUser registerUser = CreateRegisterUser(userRequestJson.Email);

            Func<Task> userResponseException = async () => { await registerUser.Execute(userRequestJson); };

            await userResponseException.Should().ThrowAsync<ValidationException>()
                .Where(exception => exception.ErrorsMessage.Count == 1 && exception.ErrorsMessage.Contains(APIMSG.EXISTING_EMAIL));
        }

        [Fact]
        public async Task Validate_Empty_Email_Error()
        {
            UserRequestJson userRequestJson = RegisterUserRequestBuilder.GenerateUserRequest();
            userRequestJson.Email = String.Empty;

            RegisterUser registerUser = CreateRegisterUser();

            Func<Task> userResponseException = async () => { await registerUser.Execute(userRequestJson); };

            await userResponseException.Should().ThrowAsync<ValidationException>()
                .Where(exception => exception.ErrorsMessage.Count == 1 && exception.ErrorsMessage.Contains(APIMSG.EMPTY_EMAIL));
        }

        private RegisterUser CreateRegisterUser(string email = "")
        {
            IUserWriteOnlyRepository userWriteOnlyRepositoryBuilder = UserWriteOnlyRepositoryBuilder.CreateInstance().UserWriteOnlyRepository();

            IMapper mapper = AutomapperConfigBuilder.CreateInstance();

            IWorkUnit workUnit = WorkUnitBuilder.CreateInstance().WorkUnit();

            PasswordEncryption passwordEncryption = PasswordEncryptionBuilder.CreateInstance();

            TokenController tokenController = TokenControllerBuilder.CreateInstance();

            IUserReadOnlyRepository userReadOnlyRepository = UserReadOnlyRepositoryBuilder.CreateInstance().GetExistingEmail(email).UserReadOnlyRepository();

            return new RegisterUser(userWriteOnlyRepositoryBuilder, mapper, workUnit, passwordEncryption, tokenController, userReadOnlyRepository);
        }
    }
}
