using System.Collections.Immutable;
using SampleProject.Application.Common.Constants;

namespace SampleProject.Application.Mvc.Filters
{
    public class AppRolePolicy
    {
        public const string USER_ACCESS = "UserAccess";
        public const string ADMINISTRATOR_ACCESS = "AdministratorAccess";

        public static readonly ImmutableArray<string> ADMINISTRATOR_ACCESS_ROLES = [AppRoleConstants.ADMINISTRATOR];
        public static readonly ImmutableArray<string> USER_ACCESS_ROLES = [AppRoleConstants.ADMINISTRATOR, AppRoleConstants.USER];
    }
}
