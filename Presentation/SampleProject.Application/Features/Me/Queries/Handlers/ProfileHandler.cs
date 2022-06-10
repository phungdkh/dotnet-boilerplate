using System.Net;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SampleProject.Application.Common.Constants;
using SampleProject.Domain;
using SampleProject.Shared.Common.Models;
using SampleProject.Application.Features.Me.Queries.Requests;
using SampleProject.Application.Features.Me.Queries.Responses;

namespace SampleProject.Application.Features.Me.Queries.Handlers
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="appDbContext"></param>
    /// <param name="contextAccessor"></param>
    public class ProfileHandler(AppDbContext appDbContext, IHttpContextAccessor contextAccessor) : BaseHandler(contextAccessor), IRequestHandler<ProfileRequest, ResponseModel>
    {
        public async Task<ResponseModel> Handle(ProfileRequest request, CancellationToken cancellationToken)
        {
            var applicationUser = await appDbContext.ApplicationUsers
                .AsNoTracking()
                .Where(u => u.Email == UserLoggedEmail)
                .ProjectToType<UserProfileResponse>()
                .FirstOrDefaultAsync(cancellationToken);

            if (applicationUser is null)
                return new ResponseModel()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = AppMessageConstants.USER_NOT_FOUND
                };

            applicationUser.Roles = string.Join(',', Roles);

            return new ResponseModel()
            {
                StatusCode = HttpStatusCode.OK,
                Data = applicationUser
            };
        }
    }
}
