using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using SampleProject.Shared.Extensions;

namespace SampleProject.Application.Mvc.Filters
{
    public class AppRolesAuthorizationHandler(
        IHttpContextAccessor contextAccessor)
        : AuthorizationHandler<AppRolesAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AppRolesAuthorizationRequirement requirement)
        {
            var roles = contextAccessor.HttpContext?.User.GetRoles();
            var userLoggedEmail = contextAccessor.HttpContext?.User.GetEmail();

            if (userLoggedEmail is null)
                return Task.CompletedTask;

            if (requirement.Roles.Length > 0 && (roles is null || roles.Count == 0))
                return Task.CompletedTask;

            if (requirement.Roles.Length == 0 || (roles is not null && roles.Any(s => requirement.Roles.Contains(s))))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}