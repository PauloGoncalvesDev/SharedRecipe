using SharedRecipe.Infrastructure.RepositoryAccess;
using TestUtility.Entities;

namespace WebApi.Test
{
    public class ContextSeedInMemory
    {
        public static (SharedRecipe.Domain.Entities.User user, string password) Seed(SharedRecipeContext sharedRecipeContext)
        {
            (SharedRecipe.Domain.Entities.User user, string password) = UserBuilder.GenerateUser();

            sharedRecipeContext.Add(user);

            sharedRecipeContext.SaveChanges();

            return (user, password);
        }
    }
}
