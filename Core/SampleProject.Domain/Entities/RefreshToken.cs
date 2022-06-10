namespace SampleProject.Domain.Entities
{
    /// <summary>
    /// Initializes new instance of <see cref="RefreshToken"/>.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="token">The refresh token.</param>
    [Table("RefreshToken")]
    public class RefreshToken(string userId, string token) : AuditableEntity
    {
        /// <summary>
        /// Gets or sets user primary key.
        /// </summary>
        [Required]
        [MaxLength(125)]
        public string UserId { get; set; } = userId;
        /// <summary>
        /// Gets or sets refresh token.
        /// </summary>
        [Required]
        [MaxLength(512)]
        public string Token { get; set; } = token;
    }
}
