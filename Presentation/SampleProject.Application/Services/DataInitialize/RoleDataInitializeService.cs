using Microsoft.AspNetCore.Identity;
using SampleProject.Domain.Entities;
using SampleProject.Application.Common.Constants;

namespace SampleProject.Application.Services.DataInitialize
{
    public class RoleDataInitializeService(RoleManager<ApplicationRole> roleManager) : IDataInitializeService
    {
        public int Order
        {
            get => 1;
            set
            {

            }
        }

        public async Task RunAsync()
        {
            foreach (var role in AppRoleConstants.ALL_ROLES)
            {
                if (await roleManager.RoleExistsAsync(role)) continue;

                await roleManager.CreateAsync(new ApplicationRole()
                {
                    Name = role,
                    NormalizedName = role
                });
            }
        }
    }
}
