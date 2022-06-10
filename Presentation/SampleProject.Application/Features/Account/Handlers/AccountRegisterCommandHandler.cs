using System.Net;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SampleProject.Application.Common.Constants;
using SampleProject.Application.Features.Account.Commands;
using SampleProject.Application.Services.Authentication;
using SampleProject.Domain;
using SampleProject.Domain.Entities;
using SampleProject.Shared.Common.Models;

namespace SampleProject.Application.Features.Account.Handlers
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sp"></param>
    public class AccountRegisterCommandHandler([FromServices] IServiceProvider sp) : IRequestHandler<AccountRegisterCommand, ResponseModel>
    {
        public async Task<ResponseModel> Handle(AccountRegisterCommand command, CancellationToken cancellationToken)
        {
            var userManager = sp.GetRequiredService<UserManager<ApplicationUser>>();
            var appDbContext = sp.GetRequiredService<AppDbContext>();
            var appAuthenticationService = sp.GetRequiredService<IAppAuthenticationService>();

            var validator = sp.GetRequiredService<IValidator<AccountRegisterCommand>>();
            await validator.ValidateAndThrowAsync(command, cancellationToken);

            if (!userManager.SupportsUserEmail)
            {
                throw new NotSupportedException($"{nameof(AccountRegisterCommandHandler)} requires a user store with email support.");
            }

            var userStore = sp.GetRequiredService<IUserStore<ApplicationUser>>();
            var emailStore = (IUserEmailStore<ApplicationUser>)userStore;
            var email = command.Email;

            var user = new ApplicationUser
            {
                PhoneNumber = command.Phone
            };

            // check and add new company
            if (!string.IsNullOrEmpty(command.CompanyName))
            {
                var company = new Company()
                {
                    Name = command.CompanyName,
                    Address = command.CompanyAddress
                };

                await appDbContext.Companies.AddAsync(company, cancellationToken);
                await appDbContext.SaveChangesAsync(cancellationToken);
                user.CompanyId = company.Id;
            }

            await userStore.SetUserNameAsync(user, email, CancellationToken.None);
            await emailStore.SetEmailAsync(user, email, CancellationToken.None);
            var result = await userManager.CreateAsync(user, command.Password);

            if (!result.Succeeded)
            {
                return new ResponseModel()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = result.Errors.First().Description
                };
            }
            await userManager.AddToRoleAsync(user, AppRoleConstants.DEFAULT_USER_REGISTER_ROLE);

            var roles = await userManager.GetRolesAsync(user);

            var authenticationResponse = await appAuthenticationService.Authenticate(user, roles, cancellationToken);

            return new ResponseModel()
            {
                StatusCode = HttpStatusCode.OK,
                Data = authenticationResponse
            };
        }
    }
}
