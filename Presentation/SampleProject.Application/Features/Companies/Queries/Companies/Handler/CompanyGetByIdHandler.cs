using System.Net;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleProject.Application.Common.Constants;
using SampleProject.Application.Features.Companies.Queries.Companies.Request;
using SampleProject.Application.Features.Companies.Queries.Companies.Response;
using SampleProject.Domain;
using SampleProject.Shared.Common.Models;

namespace SampleProject.Application.Features.Companies.Queries.Companies.Handler
{
    public class CompanyGetByIdHandler : IRequestHandler<CompanyGetByIdRequest, ResponseModel>
    {
        private readonly AppDbContext _appDbContext;

        /// <summary>
        ///   Initializes a new instance of the <see cref="CompanyGetByIdHandler" /> class.
        /// </summary>
        /// <param name="appDbContext"></param>
        public CompanyGetByIdHandler(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
        }

        public async Task<ResponseModel> Handle(CompanyGetByIdRequest request, CancellationToken cancellationToken)
        {
            var company = await _appDbContext.Companies
                .AsNoTracking()
                .Where(company => company.Id == request.Id)
                .ProjectToType<CompanyResponse>()
                .FirstOrDefaultAsync(cancellationToken);

            if (company is null)
            {
                return new ResponseModel()
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Message = AppMessageConstants.COMPANY_NOT_FOUND
                };
            }

            return new ResponseModel()
            {
                StatusCode = HttpStatusCode.OK,
                Data = company
            };
        }
    }
}
