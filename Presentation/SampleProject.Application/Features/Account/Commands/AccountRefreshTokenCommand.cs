using SampleProject.Shared.Common.Models;

namespace SampleProject.Application.Features.Account.Commands
{
    public class AccountRefreshTokenCommand : ICommandQueryBase
    {
        /// <summary>
        /// The refresh token to get new access token.
        /// </summary>
        public required string RefreshToken { get; init; }
    }
}
