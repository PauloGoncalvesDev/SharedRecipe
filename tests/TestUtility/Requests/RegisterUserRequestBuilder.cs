using Bogus;
using SharedRecipe.Reporting.Requests;

namespace TestUtility.Requests
{
    public class RegisterUserRequestBuilder
    {
        public static UserRequestJson GenerateUserRequest(int lengthPassword = 10)
        {
            return new Faker<UserRequestJson>()
                .RuleFor(r => r.Name, f => f.Person.FullName)
                .RuleFor(r => r.Email, f => f.Person.Email)
                .RuleFor(r => r.Phone, f => f.Phone.PhoneNumber("## ! ####-####").Replace("!", $"{f.Random.Int(min: 1, max: 9)}"))
                .RuleFor(r => r.Password, f => f.Internet.Password(lengthPassword));
        }
    }
}
