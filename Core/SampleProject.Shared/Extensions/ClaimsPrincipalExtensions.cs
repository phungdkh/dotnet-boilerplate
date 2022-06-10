using System.Security.Claims;

namespace SampleProject.Shared.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static List<string> GetRoles(this ClaimsPrincipal principal)
        {
            return principal.Identities.SelectMany(i =>
            {
                return i.Claims
                    .Where(c => c.Type == i.RoleClaimType)
                    .Select(c => c.Value)
                    .ToList();
            }).ToList();
        }

        public static string? GetEmail(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue("preferred_username")
                   ?? principal.FindFirstValue(ClaimTypes.Email)
                   ?? principal.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public static string? GetFullName(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.Name)
                   ?? principal.FindFirstValue(ClaimTypes.GivenName)
                   ?? principal.FindFirstValue(ClaimTypes.Surname);
        }

        public static string? GetPhone(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.MobilePhone)
                   ?? principal.FindFirstValue(ClaimTypes.HomePhone)
                   ?? principal.FindFirstValue(ClaimTypes.OtherPhone);
        }

        public static string? GetAddress(this ClaimsPrincipal principal)
        {
            return principal.FindFirstValue(ClaimTypes.StreetAddress) ?? principal.FindFirstValue("http://schemas.microsoft.com/ws/2008/06/identity/claims/address");
        }
    }
}
