namespace PhungDKH.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
