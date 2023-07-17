using Microsoft.EntityFrameworkCore;
using SharedRecipe.Domain.Entities;

namespace SharedRecipe.Infrastructure.RepositoryAccess
{
    public class SharedRecipeContext : DbContext
    {
        public SharedRecipeContext(DbContextOptions<SharedRecipeContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SharedRecipeContext).Assembly);
        }
    }
}
