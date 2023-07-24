using Moq;
using SharedRecipe.Domain.Repositories.User;

namespace TestUtility.Repositories
{
    public class UserWriteOnlyRepositoryBuilder
    {
        private static UserWriteOnlyRepositoryBuilder _instanceUserWriteOnlyRepositoryBuilder;

        private readonly Mock<IUserWriteOnlyRepository> _userWriteOnlyRepository;

        private UserWriteOnlyRepositoryBuilder()
        {
            if(_userWriteOnlyRepository == null)
                _userWriteOnlyRepository = new Mock<IUserWriteOnlyRepository>();
        }

        public static UserWriteOnlyRepositoryBuilder CreateInstance()
        {
            _instanceUserWriteOnlyRepositoryBuilder = new UserWriteOnlyRepositoryBuilder();
            return _instanceUserWriteOnlyRepositoryBuilder;
        }

        public IUserWriteOnlyRepository UserWriteOnlyRepository()
        {
            return _userWriteOnlyRepository.Object;
        }
    }
}
