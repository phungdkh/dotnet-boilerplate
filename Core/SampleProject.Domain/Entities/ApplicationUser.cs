namespace SampleProject.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override Guid Id
        {
            get => base.Id;
            set => base.Id = value;
        }

        public Guid? CompanyId { get; set; }

        public virtual Company? Company { get; set; }

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
