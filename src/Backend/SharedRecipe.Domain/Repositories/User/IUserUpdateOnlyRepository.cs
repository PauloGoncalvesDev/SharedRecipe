namespace SharedRecipe.Domain.Repositories.User
{
    public interface IUserUpdateOnlyRepository
    {
        void ChangePassword(Entities.User user);
    }
}
