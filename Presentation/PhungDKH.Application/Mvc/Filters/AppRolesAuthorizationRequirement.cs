using System.Collections.Immutable;
using Microsoft.AspNetCore.Authorization;

namespace PhungDKH.Application.Mvc.Filters
{
    public class AppRolesAuthorizationRequirement(ImmutableArray<string> roles) : IAuthorizationRequirement
    {
        public ImmutableArray<string> Roles { get; set; } = roles;
    }
}
