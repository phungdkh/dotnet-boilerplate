using System.Net;
using System.Security.Policy;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SampleProject.Application.Common.Constants;
using SampleProject.Application.Features.Companies.Commands.Companies.Request;
using SampleProject.Domain;
using SampleProject.Domain.Entities;
using SampleProject.Shared.Common.Models;

namespace SampleProject.Application.Features.Companies.Commands.Companies.Handler
{
    public class CompanyCreateHandler: BaseHandler, IRequestHandler<CompanyCreateRequest, ResponseModel>
    {
        private readonly AppDbContext _appDbContext;

        /// <summary>
        ///   Initializes a new instance of the <see cref="CompanyCreateHandler" /> class.
        /// </summary>
        /// <param name="appDbContext">The Global Administrator database context.</param>
        /// <param name="contextAccessor"></param>
        public CompanyCreateHandler(
            AppDbContext appDbContext,
            IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
            _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
        }

        public async Task<ResponseModel> Handle(CompanyCreateRequest request, CancellationToken cancellationToken)
        {
            // current user email logged in
            var userLoggedEmail = UserLoggedEmail;

            var company = request.Adapt<Company>();
            company.CreatedBy = userLoggedEmail;

            await _appDbContext.Companies.AddAsync(company, cancellationToken);

            await _appDbContext.SaveChangesAsync(cancellationToken);

            return new ResponseModel
            {
                StatusCode = HttpStatusCode.OK,
                Message = AppMessageConstants.COMPANY_CREATED_SUCCESSFULLY
            };
        }
    }
}
