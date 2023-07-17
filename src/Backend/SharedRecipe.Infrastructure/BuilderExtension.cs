using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedRecipe.Domain.Extension;
using SharedRecipe.Domain.Repositories;
using SharedRecipe.Infrastructure.RepositoryAccess;
using SharedRecipe.Infrastructure.RepositoryAccess.Repository;
using System.Reflection;

namespace SharedRecipe.Infrastructure
{
    public static class BuilderExtension
    {
        public static void AddRepository(this IServiceCollection serviceDescriptors, IConfiguration configuration)
        {
            AddFluentMigrator(serviceDescriptors, configuration);

            AddContext(serviceDescriptors, configuration);

            AddWorkUnit(serviceDescriptors);

            AddRepositoryUser(serviceDescriptors);
        }

        private static void AddContext(IServiceCollection serviceDescriptors, IConfiguration configuration)
        {
            MySqlServerVersion serverVersion = new MySqlServerVersion(new Version(8, 0, 31));

            string connectionString = configuration.GetConnectionStringsAndNameDatabase();

            serviceDescriptors.AddDbContext<SharedRecipeContext>(dbContext => dbContext.UseMySql(connectionString, serverVersion));
        }

        private static void AddWorkUnit(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddScoped<IWorkUnit, WorkUnit>();
        }

        private static void AddRepositoryUser(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddScoped<IUserReadOnlyRepository, RepositoryUser>()
                .AddScoped<IUserWriteOnlyRepository, RepositoryUser>();
        }

        private static void AddFluentMigrator(IServiceCollection serviceDescriptors, IConfiguration configuration)
        {
            serviceDescriptors.AddFluentMigratorCore().ConfigureRunner(c =>
            c.AddMySql5()
            .WithGlobalConnectionString(configuration.GetConnectionStringsAndNameDatabase()).ScanIn(Assembly.Load("SharedRecipe.Infrastructure")).For.All());
        }
    }
}
