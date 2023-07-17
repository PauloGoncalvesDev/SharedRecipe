namespace SharedRecipe.Domain.Entities
{
    public class BaseEntity
    {
        public long Id { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;

        public DateTime? UpdateDate { get; set; }
    }
}
