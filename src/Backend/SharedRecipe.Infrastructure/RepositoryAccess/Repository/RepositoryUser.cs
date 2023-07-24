using Microsoft.EntityFrameworkCore;
using SharedRecipe.Domain.Entities;
using SharedRecipe.Domain.Repositories.User;

namespace SharedRecipe.Infrastructure.RepositoryAccess.Repository
{
    public class RepositoryUser : IUserReadOnlyRepository, IUserWriteOnlyRepository, IUserUpdateOnlyRepository
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

        public async Task<User> GetUserLogin(string email, string password)
        {
            return await _sharedRecipeContext.Users.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email.Equals(email) && u.Password.Equals(password));
        }

        public void ChangePassword(User user)
        {
            _sharedRecipeContext.Users.Update(user);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _sharedRecipeContext.Users.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email.Equals(email));
        }

        public async Task<User> GetUserById(long id)
        {
            return await _sharedRecipeContext.Users.FirstOrDefaultAsync(u => u.Id == (id));
        }
    }
}
