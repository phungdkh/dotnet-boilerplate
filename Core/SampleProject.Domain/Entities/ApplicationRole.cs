namespace SampleProject.Domain.Entities
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override Guid Id
        {
            get => base.Id;
            set => base.Id = value;
        }
    }
}
