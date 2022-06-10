using SampleProject.Shared.Common.Models;

namespace SampleProject.Application.Features.Account.Commands
{
    public class AccountLoginCommand : ICommandQueryBase
    {
        /// <summary>
        /// The user's email address which acts as a user name.
        /// </summary>
        public required string Email { get; init; }

        /// <summary>
        /// The user's password.
        /// </summary>
        public required string Password { get; init; }
    }
}
