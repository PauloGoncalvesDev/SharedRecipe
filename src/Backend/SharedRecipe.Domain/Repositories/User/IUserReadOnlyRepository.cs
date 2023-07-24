using SharedRecipe.Domain.Entities;

namespace SharedRecipe.Domain.Repositories.User
{
    public interface IUserReadOnlyRepository
    {
        Task<bool> GetExistingEmail(string email);

        Task<Entities.User> GetUserLogin(string email, string password);

        Task<Entities.User> GetUserByEmail(string email);
    }
}
