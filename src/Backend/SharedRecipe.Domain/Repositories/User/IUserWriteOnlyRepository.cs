namespace SharedRecipe.Domain.Repositories.User
{
    public interface IUserWriteOnlyRepository
    {
        Task Insert(Entities.User user);
    }
}
