namespace SharedRecipe.Application.Services.LoggedUser
{
    public interface ILoggedUser
    {
        Task<Domain.Entities.User> GetLoggedUser();
    }
}
