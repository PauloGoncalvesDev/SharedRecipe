using FluentAssertions;
using SharedRecipe.Exceptions;
using SharedRecipe.Reporting.Requests;
using System.Text.Json;
using TestUtility.Requests;
using WebApi.Test.V1;
using Xunit;

namespace WebApi.Test.User.Register
{
    public class RegisterUserTest : ControllerBase
    {
        private const string METHOD = "user";

        public RegisterUserTest(SharedRecipeWebApplicationFactory<Program> sharedRecipeWebApplicationFactory) : base(sharedRecipeWebApplicationFactory) { }

        [Fact]
        public async Task Validate_End_Point_Success()
        {
            UserRequestJson userRequestJson = RegisterUserRequestBuilder.GenerateUserRequest();

            HttpResponseMessage httpResponseMessage = await PostRequest(METHOD, userRequestJson);

            httpResponseMessage.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            await using Stream responseBody = await httpResponseMessage.Content.ReadAsStreamAsync();

            JsonDocument responseBodyData = await JsonDocument.ParseAsync(responseBody);

            responseBodyData.RootElement.GetProperty("token").GetString().Should().NotBeNullOrEmpty();
            responseBodyData.RootElement.GetProperty("message").GetString().Should().NotBeNullOrEmpty();
            responseBodyData.RootElement.GetProperty("success").GetBoolean().Should().BeTrue();
        }

        [Fact]
        public async Task Validate_Empty_Name_Error()
        {
            UserRequestJson userRequestJson = RegisterUserRequestBuilder.GenerateUserRequest();
            userRequestJson.Name = String.Empty;

            HttpResponseMessage httpResponseMessage = await PostRequest(METHOD, userRequestJson);

            httpResponseMessage.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);

            await using Stream responseBody = await httpResponseMessage.Content.ReadAsStreamAsync();

            JsonDocument responseBodyData = await JsonDocument.ParseAsync(responseBody);

            responseBodyData.RootElement.GetProperty("message").EnumerateArray().Should().ContainSingle().And.Contain(c => c.GetString().Equals(APIMSG.EMPTY_NAME));
            responseBodyData.RootElement.GetProperty("success").GetBoolean().Should().BeFalse();
        }

        [Fact]
        public async Task Validate_Empty_Password_Error()
        {
            UserRequestJson userRequestJson = RegisterUserRequestBuilder.GenerateUserRequest();
            userRequestJson.Password = String.Empty;

            HttpResponseMessage httpResponseMessage = await PostRequest(METHOD, userRequestJson);

            httpResponseMessage.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);

            await using Stream responseBody = await httpResponseMessage.Content.ReadAsStreamAsync();

            JsonDocument responseBodyData = await JsonDocument.ParseAsync(responseBody);

            responseBodyData.RootElement.GetProperty("message").EnumerateArray().Should().ContainSingle().And.Contain(c => c.GetString().Equals(APIMSG.EMPTY_PASSWORD));
            responseBodyData.RootElement.GetProperty("success").GetBoolean().Should().BeFalse();
        }

        [Fact]
        public async Task Validate_Invalid_Password_Error()
        {
            UserRequestJson userRequestJson = RegisterUserRequestBuilder.GenerateUserRequest();
            userRequestJson.Password = "senha";

            HttpResponseMessage httpResponseMessage = await PostRequest(METHOD, userRequestJson);

            httpResponseMessage.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);

            await using Stream responseBody = await httpResponseMessage.Content.ReadAsStreamAsync();

            JsonDocument responseBodyData = await JsonDocument.ParseAsync(responseBody);

            responseBodyData.RootElement.GetProperty("message").EnumerateArray().Should().ContainSingle().And.Contain(c => c.GetString().Equals(APIMSG.LENGTH_PASSWORD));
            responseBodyData.RootElement.GetProperty("success").GetBoolean().Should().BeFalse();
        }

        [Fact]
        public async Task Validate_Empty_Email_Error()
        {
            UserRequestJson userRequestJson = RegisterUserRequestBuilder.GenerateUserRequest();
            userRequestJson.Email = String.Empty;

            HttpResponseMessage httpResponseMessage = await PostRequest(METHOD, userRequestJson);

            httpResponseMessage.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);

            await using Stream responseBody = await httpResponseMessage.Content.ReadAsStreamAsync();

            JsonDocument responseBodyData = await JsonDocument.ParseAsync(responseBody);

            responseBodyData.RootElement.GetProperty("message").EnumerateArray().Should().ContainSingle().And.Contain(c => c.GetString().Equals(APIMSG.EMPTY_EMAIL));
            responseBodyData.RootElement.GetProperty("success").GetBoolean().Should().BeFalse();
        }

        [Fact]
        public async Task Validate_Invalid_Email_Error()
        {
            UserRequestJson userRequestJson = RegisterUserRequestBuilder.GenerateUserRequest();
            userRequestJson.Email = "email";

            HttpResponseMessage httpResponseMessage = await PostRequest(METHOD, userRequestJson);

            httpResponseMessage.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);

            await using Stream responseBody = await httpResponseMessage.Content.ReadAsStreamAsync();

            JsonDocument responseBodyData = await JsonDocument.ParseAsync(responseBody);

            responseBodyData.RootElement.GetProperty("message").EnumerateArray().Should().ContainSingle().And.Contain(c => c.GetString().Equals(APIMSG.INVALID_EMAIL));
            responseBodyData.RootElement.GetProperty("success").GetBoolean().Should().BeFalse();
        }

        [Fact]
        public async Task Validate_Empty_Phone_Error()
        {
            UserRequestJson userRequestJson = RegisterUserRequestBuilder.GenerateUserRequest();
            userRequestJson.Phone = String.Empty;

            HttpResponseMessage httpResponseMessage = await PostRequest(METHOD, userRequestJson);

            httpResponseMessage.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);

            await using Stream responseBody = await httpResponseMessage.Content.ReadAsStreamAsync();

            JsonDocument responseBodyData = await JsonDocument.ParseAsync(responseBody);

            responseBodyData.RootElement.GetProperty("message").EnumerateArray().Should().ContainSingle().And.Contain(c => c.GetString().Equals(APIMSG.EMPTY_PHONE));
            responseBodyData.RootElement.GetProperty("success").GetBoolean().Should().BeFalse();
        }

        [Fact]
        public async Task Validate_Invalid_Phone_Error()
        {
            UserRequestJson userRequestJson = RegisterUserRequestBuilder.GenerateUserRequest();
            userRequestJson.Phone = "88996395102";

            HttpResponseMessage httpResponseMessage = await PostRequest(METHOD, userRequestJson);

            httpResponseMessage.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);

            await using Stream responseBody = await httpResponseMessage.Content.ReadAsStreamAsync();

            JsonDocument responseBodyData = await JsonDocument.ParseAsync(responseBody);

            responseBodyData.RootElement.GetProperty("message").EnumerateArray().Should().ContainSingle().And.Contain(c => c.GetString().Equals(APIMSG.FORMAT_PHONE));
            responseBodyData.RootElement.GetProperty("success").GetBoolean().Should().BeFalse();
        }
    }
}
