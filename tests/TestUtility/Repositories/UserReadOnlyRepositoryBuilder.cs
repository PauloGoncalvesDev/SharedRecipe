using Moq;
using SharedRecipe.Domain.Repositories;

namespace TestUtility.Repositories
{
    public class UserReadOnlyRepositoryBuilder
    {
        private static UserReadOnlyRepositoryBuilder _instanceUserReadOnlyRepositoryBuilder;

        private readonly Mock<IUserReadOnlyRepository> _userReadOnlyRepository;

        private UserReadOnlyRepositoryBuilder()
        {
            if (_userReadOnlyRepository == null)
                _userReadOnlyRepository = new Mock<IUserReadOnlyRepository>();
        }

        public static UserReadOnlyRepositoryBuilder CreateInstance()
        {
            _instanceUserReadOnlyRepositoryBuilder = new UserReadOnlyRepositoryBuilder();
            return _instanceUserReadOnlyRepositoryBuilder;
        }

        public UserReadOnlyRepositoryBuilder GetExistingEmail(string email)
        {
            if(!string.IsNullOrEmpty(email))
                _userReadOnlyRepository.Setup(i => i.GetExistingEmail(email)).ReturnsAsync(true);

            return this;
        }

        public IUserReadOnlyRepository UserReadOnlyRepository()
        {
            return _userReadOnlyRepository.Object;
        }
    }
}
