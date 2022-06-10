namespace SampleProject.Domain
{
    public class AppDbContext(DbContextOptions<AppDbContext> options)
        : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options)
    {
        public virtual required DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual required DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public virtual required DbSet<Company> Companies { get; set; }
        public virtual required DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
