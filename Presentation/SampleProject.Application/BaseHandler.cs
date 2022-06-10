using Microsoft.AspNetCore.Http;
using SampleProject.Shared.Extensions;

namespace SampleProject.Application
{
    public class BaseHandler(IHttpContextAccessor contextAccessor)
    {
        public IEnumerable<string> Roles { get; set; } = contextAccessor.HttpContext?.User.GetRoles() ?? throw new InvalidOperationException();

        public string UserLoggedEmail { get; set; } = contextAccessor.HttpContext?.User.GetFullName() ?? throw new InvalidOperationException();
    }
}
