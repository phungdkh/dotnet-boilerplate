using SampleProject.Shared.Common.Models;

namespace SampleProject.Application.Features.Account.Commands
{
    public class AccountRegisterCommand : ICommandQueryBase
    {
        /// <summary>
        /// The user's email address which acts as a username.
        /// </summary>
        public required string Email { get; init; }

        /// <summary>
        /// The user's email address which acts as a phone number.
        /// </summary>
        public required string Phone { get; init; }

        /// <summary>
        /// The user's password.
        /// </summary>
        public required string Password { get; init; }

        public string CompanyName { get; set; } = string.Empty;

        public string CompanyAddress { get; set; } = string.Empty;
    }
}
