using System.Collections.Immutable;

namespace SampleProject.Application.Common.Constants
{
    public static class AppRoleConstants
    {
        public const string ADMINISTRATOR = "Administrator";

        public const string USER = "User";

        public const string DEFAULT_USER_REGISTER_ROLE = USER;

        public static readonly ImmutableArray<string> ALL_ROLES = [AppRoleConstants.ADMINISTRATOR, AppRoleConstants.USER];
    }
}
