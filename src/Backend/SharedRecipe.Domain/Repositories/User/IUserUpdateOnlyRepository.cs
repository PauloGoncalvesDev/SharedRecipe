﻿namespace SharedRecipe.Domain.Repositories.User
{
    public interface IUserUpdateOnlyRepository
    {
        void ChangePassword(Entities.User user);

        Task<Entities.User> GetUserById(long id);
    }
}
