using SharedRecipe.Domain.Repositories;

namespace SharedRecipe.Infrastructure.RepositoryAccess
{
    public sealed class WorkUnit : IDisposable, IWorkUnit
    {
        private readonly SharedRecipeContext _sharedRecipeContext;

        private bool _dispose;

        public WorkUnit(SharedRecipeContext sharedRecipeContext)
        {
            _sharedRecipeContext = sharedRecipeContext;
        }

        public async Task Commit()
        {
            await _sharedRecipeContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public void Dispose(bool disposing)
        {
            if (disposing && !_dispose)
                _sharedRecipeContext.Dispose();

            _dispose = true;
        }
    }
}
