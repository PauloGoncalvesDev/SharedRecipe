using Microsoft.Extensions.Configuration;

namespace SharedRecipe.Domain.Extension
{
    public static class ExtensionRepository
    {
        public static string GetConnectionStrings(this IConfiguration configuration)
        {
            string connectionStrings = configuration.GetConnectionString("Connection");

            return connectionStrings;
        }

        public static string GetNameDatabase(this IConfiguration configuration)
        {
            string nameDatabase = configuration.GetConnectionString("NameDatabase");

            return nameDatabase;
        }

        public static string GetConnectionStringsAndNameDatabase(this IConfiguration configuration)
        {
            return $"{configuration.GetConnectionStrings()}Database={configuration.GetNameDatabase()}";
        }
    }
}
