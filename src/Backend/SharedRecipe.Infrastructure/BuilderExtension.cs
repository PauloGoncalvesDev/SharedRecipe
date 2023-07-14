using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedRecipe.Domain.Extension;
using System.Reflection;

namespace SharedRecipe.Infrastructure
{
    public static class BuilderExtension
    {
        public static void AddRepository(this IServiceCollection serviceDescriptors, IConfiguration configuration)
        {
            AddFluentMigrator(serviceDescriptors, configuration);
        }

        private static void AddFluentMigrator(IServiceCollection serviceDescriptors, IConfiguration configuration)
        {
            serviceDescriptors.AddFluentMigratorCore().ConfigureRunner(c =>
            c.AddMySql5()
            .WithGlobalConnectionString(configuration.GetConnectionStringsAndNameDatabase()).ScanIn(Assembly.Load("SharedRecipe.Infrastructure")).For.All());
        }
    }
}
