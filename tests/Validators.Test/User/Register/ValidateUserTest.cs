using FluentAssertions;
using FluentValidation.Results;
using SharedRecipe.Application.Validators.User;
using SharedRecipe.Exceptions;
using SharedRecipe.Reporting.Requests;
using TestUtility.Requests;
using Xunit;

namespace Validators.Test.User.Register
{
    public class ValidateUserTest
    {
        [Fact]
        public void Validate_Request_Success()
        {
            UserRequestJson userRequestJson = RegisterUserRequestBuilder.GenerateUserRequest();

            ValidationResult validationResult = new ValidateUser().Validate(userRequestJson);

            validationResult.IsValid.Should().BeTrue();
            validationResult.Errors.Should().BeEmpty();
        }

        [Fact]
        public void Validate_Empty_Name_Error()
        {
            UserRequestJson userRequestJson = RegisterUserRequestBuilder.GenerateUserRequest();
            userRequestJson.Name = String.Empty;

            ValidationResult validationResult = new ValidateUser().Validate(userRequestJson);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle().And.Contain(m => m.ErrorMessage.Equals(APIMSG.EMPTY_NAME));
        }

        [Fact]
        public void Validate_Empty_Email_Error()
        {
            UserRequestJson userRequestJson = RegisterUserRequestBuilder.GenerateUserRequest();
            userRequestJson.Email = String.Empty;

            ValidationResult validationResult = new ValidateUser().Validate(userRequestJson);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle().And.Contain(m => m.ErrorMessage.Equals(APIMSG.EMPTY_EMAIL));
        }

        [Fact]
        public void Validate_Invalid_Email_Error()
        {
            UserRequestJson userRequestJson = RegisterUserRequestBuilder.GenerateUserRequest();
            userRequestJson.Email = "email";

            ValidationResult validationResult = new ValidateUser().Validate(userRequestJson);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle().And.Contain(m => m.ErrorMessage.Equals(APIMSG.INVALID_EMAIL));
        }

        [Fact]
        public void Validate_Empty_Phone_Error()
        {
            UserRequestJson userRequestJson = RegisterUserRequestBuilder.GenerateUserRequest();
            userRequestJson.Phone = String.Empty;

            ValidationResult validationResult = new ValidateUser().Validate(userRequestJson);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle().And.Contain(m => m.ErrorMessage.Equals(APIMSG.EMPTY_PHONE));
        }

        [Fact]
        public void Validate_Invalid_Phone_Error()
        {
            UserRequestJson userRequestJson = RegisterUserRequestBuilder.GenerateUserRequest();
            userRequestJson.Phone = "88996919043";

            ValidationResult validationResult = new ValidateUser().Validate(userRequestJson);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle().And.Contain(m => m.ErrorMessage.Equals(APIMSG.FORMAT_PHONE));
        }

        [Fact]
        public void Validate_Empty_Password_Error()
        {
            UserRequestJson userRequestJson = RegisterUserRequestBuilder.GenerateUserRequest();
            userRequestJson.Password = String.Empty;

            ValidationResult validationResult = new ValidateUser().Validate(userRequestJson);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle().And.Contain(m => m.ErrorMessage.Equals(APIMSG.EMPTY_PASSWORD));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void Validate_Invalid_Password_Error(int lengthPassword)
        {
            UserRequestJson userRequestJson = RegisterUserRequestBuilder.GenerateUserRequest(lengthPassword);

            ValidationResult validationResult = new ValidateUser().Validate(userRequestJson);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().ContainSingle().And.Contain(m => m.ErrorMessage.Equals(APIMSG.LENGTH_PASSWORD));
        }
    }
}
