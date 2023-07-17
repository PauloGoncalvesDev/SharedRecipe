using Microsoft.Extensions.DependencyInjection;
using SharedRecipe.Application.BusinessRules.User.Register;

namespace SharedRecipe.Application
{
    public static class BuilderExtension
    {
        public static void AddApplication(this IServiceCollection serviceDescriptors)
        {
            AddApplicationUser(serviceDescriptors);
        }

        private static void AddApplicationUser(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddScoped<IRegisterUser, RegisterUser>();
        }
    }
}
