using Newtonsoft.Json;
using SharedRecipe.Exceptions;
using SharedRecipe.Reporting.Requests;
using System.Globalization;
using System.Text;
using Xunit;

namespace WebApi.Test.V1
{
    public class ControllerBase : IClassFixture<SharedRecipeWebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;

        public ControllerBase(SharedRecipeWebApplicationFactory<Program> sharedRecipeWebApplicationFactory)
        {
            _httpClient = sharedRecipeWebApplicationFactory.CreateClient();
            APIMSG.Culture = CultureInfo.CurrentCulture;
        }

        protected async Task<HttpResponseMessage> PostRequest(string methodName, UserRequestJson requestBody)
        {
            return await _httpClient.PostAsync(methodName, new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json"));
        }

        protected async Task<HttpResponseMessage> PostRequest(string methodName, UserLoginRequestJson userLoginRequestJson)
        {
            return await _httpClient.PostAsync(methodName, new StringContent(JsonConvert.SerializeObject(userLoginRequestJson), Encoding.UTF8, "application/json"));
        }
    }
}
