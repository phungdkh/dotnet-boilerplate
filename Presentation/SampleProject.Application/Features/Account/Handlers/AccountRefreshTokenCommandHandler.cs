using System.Net;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SampleProject.Application.Features.Account.Commands;
using SampleProject.Application.Services.Authentication;
using SampleProject.Domain;
using SampleProject.Domain.Entities;
using SampleProject.Shared.Common.Models;
using SampleProject.Shared.Exceptions;

namespace SampleProject.Application.Features.Account.Handlers
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sp"></param>
    public class AccountRefreshTokenCommandHandler([FromServices] IServiceProvider sp) : IRequestHandler<AccountRefreshTokenCommand, ResponseModel>
    {
        public async Task<ResponseModel> Handle(AccountRefreshTokenCommand command, CancellationToken cancellationToken)
        {
            var appDbContext = sp.GetRequiredService<AppDbContext>();
            var userManager = sp.GetRequiredService<UserManager<ApplicationUser>>();
            var appAuthenticationService = sp.GetRequiredService<IAppAuthenticationService>();

            var isValidRefreshToken = appAuthenticationService.Validate(command.RefreshToken);
            if (!isValidRefreshToken)
                throw new InvalidRefreshTokenException();

            var refreshToken =
                await appDbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == command.RefreshToken,
                    cancellationToken) ?? throw new InvalidRefreshTokenException();

            appDbContext.RefreshTokens.Remove(refreshToken);
            await appDbContext.SaveChangesAsync(cancellationToken);

            var user = await userManager.FindByIdAsync(refreshToken.UserId) ?? throw new UserNotFoundException();
            var roles = await userManager.GetRolesAsync(user);

            return new ResponseModel()
            {
                StatusCode = HttpStatusCode.OK,
                Data = await appAuthenticationService.Authenticate(user, roles, cancellationToken)
            };
        }
    }
}
