using Bogus;
using SharedRecipe.Domain.Entities;
using TestUtility.Cryptography;

namespace TestUtility.Entities
{
    public class UserBuilder
    {

        public static (User user, string password) GenerateUser()
        {
            string password = string.Empty;

            User user = new Faker<User>()
                .RuleFor(r => r.Id, _ => 1)
                .RuleFor(r => r.Name, f => f.Person.FullName)
                .RuleFor(r => r.Email, f => f.Person.Email)
                .RuleFor(r => r.Phone, f => f.Phone.PhoneNumber("## ! ####-####").Replace("!", $"{f.Random.Int(min: 1, max: 9)}"))
                .RuleFor(r => r.Password, f =>
                {
                    password = f.Internet.Password();

                    return PasswordEncryptionBuilder.CreateInstance().Encrypt(password);
                });

            return (user, password);
        }
    }
}
