using System.Net;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SampleProject.Application.Features.Companies.Queries.Companies.Request;
using SampleProject.Application.Features.Companies.Queries.Companies.Response;
using SampleProject.Domain;
using SampleProject.Shared.Common.Models;

namespace SampleProject.Application.Features.Companies.Queries.Companies.Handler
{
    public class CompanyListHandler : BaseHandler, IRequestHandler<CompanyListRequest, ResponseModel>
    {
        private readonly AppDbContext _appDbContext;

        /// <summary>
        ///   Initializes a new instance of the <see cref="CompanyListHandler" /> class.
        /// </summary>
        /// <param name="appDbContext"></param>
        /// <param name="contextAccessor"></param>

        public CompanyListHandler(
            AppDbContext appDbContext,
            IHttpContextAccessor contextAccessor
            )
            : base(contextAccessor)
        {
            _appDbContext = appDbContext;
        }

        public async Task<ResponseModel> Handle(CompanyListRequest request, CancellationToken cancellationToken)
        {
            var companies = await _appDbContext.Companies
                .AsNoTracking()
                .OrderByDescending(company => company.CreatedOn)
                .ProjectToType<CompanyResponse>()
                .ToListAsync(cancellationToken);

            return new ResponseModel
            {
                StatusCode = HttpStatusCode.OK,
                Data = companies
            };
        }
    }
}
