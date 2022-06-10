namespace SampleProject.Domain.Entities
{
    [Table("Company")]
    public class Company : AuditableEntity
    {
        [Required]
        [MaxLength(255)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(255)]
        public string LogoUrl { get; set; } = string.Empty;

        [MaxLength(255)]
        public string Address { get; set; } = string.Empty;

        [MaxLength(125)]
        public string PhoneNumber { get; set; } = string.Empty;

        [MaxLength(125)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(512)]
        public string Description { get; set; } = string.Empty;

        public ICollection<ApplicationUser> ApplicationUsers { get; set; } = [];
    }
}
