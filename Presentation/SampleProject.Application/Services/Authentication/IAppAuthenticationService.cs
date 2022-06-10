using SampleProject.Application.Services.Authentication.DTO;
using SampleProject.Domain.Entities;

namespace SampleProject.Application.Services.Authentication
{
    /// <summary>
    /// Interface for authentication.
    /// </summary>
    public interface IAppAuthenticationService
    {
        /// <summary>
        /// Authenticates user.
        /// Takes responsibilities to generate access and refresh token, save refresh token in database
        /// and return instance of <see cref="AuthenticateResponse"/> class. 
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="roles">The roles of user.</param>
        /// <param name="cancellationToken">Instance of <see cref="CancellationToken"/>.</param>
        Task<AuthenticateResponse> Authenticate(ApplicationUser user, IList<string> roles, CancellationToken cancellationToken);

        /// <summary>
        /// Validates refresh token.
        /// </summary>
        /// <param name="refreshToken">The refresh token.</param>
        /// <returns>True if token is valid,otherwise false.</returns>
        bool Validate(string refreshToken);
    }
}
