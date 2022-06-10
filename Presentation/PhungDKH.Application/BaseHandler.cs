using Microsoft.AspNetCore.Http;
using PhungDKH.Shared.Extensions;

namespace PhungDKH.Application
{
    public class BaseHandler(IHttpContextAccessor contextAccessor)
    {
        public IEnumerable<string> Roles { get; set; } = contextAccessor.HttpContext?.User.GetRoles() ?? throw new InvalidOperationException();

        public string UserLoggedEmail { get; set; } = contextAccessor.HttpContext?.User.GetFullName() ?? throw new InvalidOperationException();
    }
}
