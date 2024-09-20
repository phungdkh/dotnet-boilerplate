using System.Net;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SampleProject.Application.Common.Constants;
using SampleProject.Application.Features.Companies.Commands.Companies.Request;
using SampleProject.Domain;
using SampleProject.Shared.Common.Models;

namespace SampleProject.Application.Features.Companies.Commands.Companies.Handler
{
    public class CompanyEditHandler : BaseHandler, IRequestHandler<CompanyEditRequest, ResponseModel>
    {
        private readonly AppDbContext _appDbContext;

        /// <summary>
        ///   Initializes a new instance of the <see cref="CompanyEditHandler" /> class.
        /// </summary>
        /// <param name="appDbContext">The Global Administrator database context.</param>
        /// <param name="contextAccessor"></param>
        public CompanyEditHandler(
            AppDbContext appDbContext,
            IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
            _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
        }

        public async Task<ResponseModel> Handle(CompanyEditRequest request, CancellationToken cancellationToken)
        {
            var company = await _appDbContext.Companies.FirstOrDefaultAsync(_ => _.Id == request.Id, cancellationToken);
            if (company is null) return new ResponseModel()
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = AppMessageConstants.COMPANY_NOT_FOUND
            };

            request.Adapt(company);

            // current user email logged in
            var userLoggedEmail = UserLoggedEmail;
            company.UpdatedBy = userLoggedEmail;
            company.UpdatedOn = DateTime.UtcNow;
            
            await _appDbContext.SaveChangesAsync(cancellationToken);


            return new ResponseModel
            {
                StatusCode = HttpStatusCode.OK,
                Message = AppMessageConstants.COMPANY_UPDATED_SUCCESSFULLY,
            };
        }
    }
}
