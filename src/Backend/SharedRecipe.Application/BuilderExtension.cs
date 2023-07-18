using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedRecipe.Application.BusinessRules.User.Register;
using SharedRecipe.Application.Services.Cryptography;

namespace SharedRecipe.Application
{
    public static class BuilderExtension
    {
        public static void AddApplication(this IServiceCollection serviceDescriptors, IConfiguration configuration)
        {
            AddApplicationServicePasswordEncryption(serviceDescriptors, configuration);

            AddApplicationUser(serviceDescriptors);
        }

        private static void AddApplicationUser(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddScoped<IRegisterUser, RegisterUser>();
        }

        private static void AddApplicationServicePasswordEncryption(IServiceCollection serviceDescriptors, IConfiguration configuration)
        {
            string internPassword = configuration.GetRequiredSection("Configuration:InternPassword").Value ?? string.Empty;

            serviceDescriptors.AddScoped(options => new PasswordEncryption(internPassword));
        }
    }
}
