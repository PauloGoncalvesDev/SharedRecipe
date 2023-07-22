using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharedRecipe.Infrastructure.RepositoryAccess;

namespace WebApi.Test
{
    public class SharedRecipeWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        private SharedRecipe.Domain.Entities.User _user;

        private string _password;

        protected override void ConfigureWebHost(IWebHostBuilder webHostBuilder)
        {
            webHostBuilder.UseEnvironment("Test")
                .ConfigureServices(services =>
                {
                    ServiceDescriptor serviceDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(SharedRecipeContext));
                    if(serviceDescriptor != null)
                        services.Remove(serviceDescriptor);

                    ServiceProvider serviceProvider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                    services.AddDbContext<SharedRecipeContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                        options.UseInternalServiceProvider(serviceProvider);
                    });

                    using IServiceScope scopeServiceProvider = services.BuildServiceProvider().CreateScope();

                    SharedRecipeContext sharedRecipeContext = scopeServiceProvider.ServiceProvider.GetRequiredService<SharedRecipeContext>();

                    sharedRecipeContext.Database.EnsureDeleted();

                    (_user, _password) = ContextSeedInMemory.Seed(sharedRecipeContext);
                });
        }

        public SharedRecipe.Domain.Entities.User GetSeedUser()
        {
            return _user;
        }

        public string GetSeedPassword()
        {
            return _password;
        }
    }
}
