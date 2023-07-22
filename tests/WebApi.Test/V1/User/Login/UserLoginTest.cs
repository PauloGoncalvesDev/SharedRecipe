using FluentAssertions;
using SharedRecipe.Exceptions;
using SharedRecipe.Reporting.Requests;
using System.Text.Json;
using TestUtility.Entities;
using Xunit;

namespace WebApi.Test.V1.User.Login
{
    public class UserLoginTest : ControllerBase
    {
        private const string METHOD = "login";

        private readonly SharedRecipe.Domain.Entities.User _user;

        private readonly string _password;

        public UserLoginTest(SharedRecipeWebApplicationFactory<Program> sharedRecipeWebApplicationFactory) : base(sharedRecipeWebApplicationFactory) 
        {
            _user = sharedRecipeWebApplicationFactory.GetSeedUser();
            _password = sharedRecipeWebApplicationFactory.GetSeedPassword();
        }

        [Fact]
        public async Task Validate_End_Point_Success()
        {
            HttpResponseMessage httpResponseMessage = await PostRequest(METHOD, new UserLoginRequestJson
            {
                Password = _password,
                Email = _user.Email
            });

            httpResponseMessage.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            await using Stream responseBody = await httpResponseMessage.Content.ReadAsStreamAsync();

            JsonDocument responseBodyData = await JsonDocument.ParseAsync(responseBody);

            responseBodyData.RootElement.GetProperty("token").GetString().Should().NotBeNullOrEmpty();
            responseBodyData.RootElement.GetProperty("name").GetString().Should().NotBeNullOrEmpty().And.Be(_user.Name);
            responseBodyData.RootElement.GetProperty("success").GetBoolean().Should().BeTrue();
            responseBodyData.RootElement.GetProperty("message").GetString().Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Validate_Invalid_Email_Error()
        {
            HttpResponseMessage httpResponseMessage = await PostRequest(METHOD, new UserLoginRequestJson
            {
                Password = _password,
                Email = "emailerrado@errado.com"
            });

            httpResponseMessage.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);

            await using Stream responseBody = await httpResponseMessage.Content.ReadAsStreamAsync();

            JsonDocument responseBodyData = await JsonDocument.ParseAsync(responseBody);

            responseBodyData.RootElement.GetProperty("success").GetBoolean().Should().BeFalse();
            responseBodyData.RootElement.GetProperty("message").EnumerateArray().Should().ContainSingle().And.Contain(c => c.GetString().Equals(APIMSG.LOGIN_ERROR));
        }

        [Fact]
        public async Task Validate_Invalid_Password_Error()
        {
            HttpResponseMessage httpResponseMessage = await PostRequest(METHOD, new UserLoginRequestJson
            {
                Password = "invalidpassword",
                Email = _user.Email
            });

            httpResponseMessage.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);

            await using Stream responseBody = await httpResponseMessage.Content.ReadAsStreamAsync();

            JsonDocument responseBodyData = await JsonDocument.ParseAsync(responseBody);

            responseBodyData.RootElement.GetProperty("success").GetBoolean().Should().BeFalse();
            responseBodyData.RootElement.GetProperty("message").EnumerateArray().Should().ContainSingle().And.Contain(c => c.GetString().Equals(APIMSG.LOGIN_ERROR));
        }

        [Fact]
        public async Task Validate_Invalid_Email_And_Password_Error()
        {
            HttpResponseMessage httpResponseMessage = await PostRequest(METHOD, new UserLoginRequestJson
            {
                Email = "emailerrado@errado.com",
                Password = "invalidpassword"
            });

            httpResponseMessage.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);

            await using Stream responseBody = await httpResponseMessage.Content.ReadAsStreamAsync();

            JsonDocument responseBodyData = await JsonDocument.ParseAsync(responseBody);

            responseBodyData.RootElement.GetProperty("success").GetBoolean().Should().BeFalse();
            responseBodyData.RootElement.GetProperty("message").EnumerateArray().Should().ContainSingle().And.Contain(c => c.GetString().Equals(APIMSG.LOGIN_ERROR));
        }
    }
}
