using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SampleProject.Domain.Entities;
using SampleProject.Application.Common.Constants;

namespace SampleProject.Application.Services.DataInitialize
{
    public class UserDataInitializeService(UserManager<ApplicationUser> userManager,
        [FromServices] IServiceProvider sp) : IDataInitializeService
    {
        public int Order
        {
            get => 2;
            set
            {

            }
        }

        public async Task RunAsync()
        {
            var userStore = sp.GetRequiredService<IUserStore<ApplicationUser>>();
            var emailStore = (IUserEmailStore<ApplicationUser>)userStore;
            var users = new List<ApplicationUser>()
            {
                new()
                {
                    Email = "admin@yourcompany.com",
                    UserName = "admin@yourcompany.com",
                    NormalizedEmail = "admin@yourcompany.com",
                    NormalizedUserName = "admin@yourcompany.com"
                },
                new()
                {
                    Email = "staff@yourcompany.com",
                    UserName = "staff@yourcompany.com",
                    NormalizedEmail = "staff@yourcompany.com",
                    NormalizedUserName = "staff@yourcompany.com"
                }
            };
            foreach (var user in users)
            {
                await userStore.SetUserNameAsync(user, user.Email, CancellationToken.None);
                await emailStore.SetEmailAsync(user, user.Email, CancellationToken.None);
                var result = await userManager.CreateAsync(user, "Pass@123123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "admin@yourcompany.com".Equals(user.Email)
                        ? AppRoleConstants.ADMINISTRATOR
                        : AppRoleConstants.USER);
                }
            }
        }
    }
}
