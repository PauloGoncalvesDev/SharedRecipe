using SharedRecipe.Domain.Entities;

namespace SharedRecipe.Domain.Repositories
{
    public interface IUserWriteOnlyRepository
    {
        Task Insert(User user);
    }
}
