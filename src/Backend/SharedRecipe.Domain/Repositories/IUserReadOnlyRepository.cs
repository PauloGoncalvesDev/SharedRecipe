using SharedRecipe.Domain.Entities;

namespace SharedRecipe.Domain.Repositories
{
    public interface IUserReadOnlyRepository
    {
        Task<bool> GetExistingEmail(string email);
    }
}
