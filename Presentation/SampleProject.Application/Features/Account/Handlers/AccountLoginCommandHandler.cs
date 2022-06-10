using System.Net;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SampleProject.Application.Features.Account.Commands;
using SampleProject.Application.Services.Authentication;
using SampleProject.Domain.Entities;
using SampleProject.Shared.Common.Models;

namespace SampleProject.Application.Features.Account.Handlers
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sp"></param>
    public class AccountLoginCommandHandler([FromServices] IServiceProvider sp) : IRequestHandler<AccountLoginCommand, ResponseModel>
    {
        public async Task<ResponseModel> Handle(AccountLoginCommand command, CancellationToken cancellationToken)
        {
            var signInManager = sp.GetRequiredService<SignInManager<ApplicationUser>>();
            var userManager = sp.GetRequiredService<UserManager<ApplicationUser>>();
            var appAuthenticationService = sp.GetRequiredService<IAppAuthenticationService>();

            var validator = sp.GetRequiredService<IValidator<AccountLoginCommand>>();
            await validator.ValidateAndThrowAsync(command, cancellationToken);

            var user = await signInManager.UserManager.FindByEmailAsync(command.Email);

            if (user is null)
                return new ResponseModel()
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "Có lỗi trong quá trình xác thực thông tin đăng nhập. Vui lòng thử lại sau!"
                };

            signInManager.AuthenticationScheme = IdentityConstants.BearerScheme;
            var result = await signInManager.CheckPasswordSignInAsync(user, command.Password, false);
            if (!result.Succeeded)
            {
                return new ResponseModel()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Message = result.ToString()
                };
            }
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
