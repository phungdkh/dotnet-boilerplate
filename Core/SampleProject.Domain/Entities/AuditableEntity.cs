namespace SampleProject.Domain.Entities
{
    public class AuditableEntity : BaseEntity
    {
        public bool IsDeleted { get; set; }

        [MaxLength(255)]
        public string CreatedBy { get; set; } = string.Empty;

        public DateTimeOffset? CreatedOn { get; set; }

        [MaxLength(255)]
        public string UpdatedBy { get; set; } = string.Empty;

        public DateTimeOffset? UpdatedOn { get; set; }

        [MaxLength(255)]
        public string DeletedBy { get; set; } = string.Empty;

        public DateTimeOffset? DeletedOn { get; set; }
    }
}
