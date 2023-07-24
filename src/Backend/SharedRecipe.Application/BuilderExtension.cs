using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedRecipe.Application.BusinessRules.User.ChangePassword;
using SharedRecipe.Application.BusinessRules.User.Login;
using SharedRecipe.Application.BusinessRules.User.Register;
using SharedRecipe.Application.Services.Cryptography;
using SharedRecipe.Application.Services.LoggedUser;
using SharedRecipe.Application.Services.Token;

namespace SharedRecipe.Application
{
    public static class BuilderExtension
    {
        public static void AddApplication(this IServiceCollection serviceDescriptors, IConfiguration configuration)
        {
            AddApplicationServicePasswordEncryption(serviceDescriptors, configuration);

            AddApplicationServiceTokenJwt(serviceDescriptors, configuration);

            AddApplicationUser(serviceDescriptors);

            AddApplicationLoggedUser(serviceDescriptors);
        }

        private static void AddApplicationUser(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddScoped<IRegisterUser, RegisterUser>()
                .AddScoped<IUserLogin, UserLogin>()
                .AddScoped<IChangePassword, ChangePassword>();
        }

        private static void AddApplicationServicePasswordEncryption(IServiceCollection serviceDescriptors, IConfiguration configuration)
        {
            string internPassword = configuration.GetRequiredSection("Configuration:InternPassword").Value ?? string.Empty;

            serviceDescriptors.AddScoped(options => new PasswordEncryption(internPassword));
        }

        private static void AddApplicationServiceTokenJwt(IServiceCollection serviceDescriptors, IConfiguration configuration)
        {
            int expirationTime = Convert.ToInt32(configuration.GetRequiredSection("Configuration:JwtExpirationTime").Value);

            string securityPassword = configuration.GetRequiredSection("Configuration:JwtSecurityPassword").Value;

            serviceDescriptors.AddScoped(options => new TokenController(expirationTime, securityPassword));
        }

        private static void AddApplicationLoggedUser(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddScoped<ILoggedUser, LoggedUser>();
        }
    }
}
