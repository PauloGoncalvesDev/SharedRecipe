using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace SharedRecipe.Infrastructure.Migrations
{
    public static class MigrationExtension
    {
        public static void MigrateDatabase(this IApplicationBuilder app)
        {
            using IServiceScope serviceScope = app.ApplicationServices.CreateScope();

            IMigrationRunner migrationRunner = serviceScope.ServiceProvider.GetRequiredService<IMigrationRunner>();

            migrationRunner.ListMigrations();

            migrationRunner.MigrateUp();
        }
    }
}
