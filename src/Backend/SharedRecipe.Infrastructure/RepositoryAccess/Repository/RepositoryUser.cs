using Microsoft.EntityFrameworkCore;
using SharedRecipe.Domain.Entities;
using SharedRecipe.Domain.Repositories;

namespace SharedRecipe.Infrastructure.RepositoryAccess.Repository
{
    public class RepositoryUser : IUserReadOnlyRepository, IUserWriteOnlyRepository
    {
        private readonly SharedRecipeContext _sharedRecipeContext;

        public RepositoryUser(SharedRecipeContext sharedRecipeContext)
        {
            _sharedRecipeContext = sharedRecipeContext;
        }

        public async Task<bool> GetExistingEmail(string email)
        {
            return await _sharedRecipeContext.Users.AnyAsync(u => u.Email.Equals(email));
        }

        public async Task Insert(User user)
        {
            await _sharedRecipeContext.Users.AddAsync(user);
        }
    }
}
