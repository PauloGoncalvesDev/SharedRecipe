using Moq;
using SharedRecipe.Domain.Repositories;

namespace TestUtility.Repositories
{
    public class WorkUnitBuilder
    {
        private static WorkUnitBuilder _instanceWorkUnitBuilder;

        private readonly Mock<IWorkUnit> _workUnit;

        private WorkUnitBuilder()
        {
            if (_workUnit == null)
                _workUnit = new Mock<IWorkUnit>();
        }

        public static WorkUnitBuilder CreateInstance()
        {
            _instanceWorkUnitBuilder = new WorkUnitBuilder();
            return _instanceWorkUnitBuilder;
        }

        public IWorkUnit WorkUnit()
        {
            return _workUnit.Object;
        }
    }
}
