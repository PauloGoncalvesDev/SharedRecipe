using SharedRecipe.Application.Services.Cryptography;

namespace TestUtility.Cryptography
{
    public class PasswordEncryptionBuilder
    {
        public static PasswordEncryption CreateInstance()
        {
            return new PasswordEncryption("4384inads0");
        }
    }
}
