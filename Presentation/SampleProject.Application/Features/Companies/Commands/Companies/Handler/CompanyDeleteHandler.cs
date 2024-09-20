using System.Net;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SampleProject.Application.Common.Constants;
using SampleProject.Application.Features.Companies.Commands.Companies.Request;
using SampleProject.Domain;
using SampleProject.Shared.Common.Models;

namespace SampleProject.Application.Features.Companies.Commands.Companies.Handler
{
    public class CompanyDeleteHandler : BaseHandler, IRequestHandler<CompanyDeleteRequest, ResponseModel>
    {
        private readonly AppDbContext _appDbContext;

        /// <summary>
        ///   Initializes a new instance of the <see cref="CompanyDeleteHandler" /> class.
        /// </summary>
        /// <param name="db">The database context.</param>
        /// <param name="contextAccessor"></param>
        public CompanyDeleteHandler(
            AppDbContext db,
            IHttpContextAccessor contextAccessor) : base(contextAccessor)
        {
            _appDbContext = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<ResponseModel> Handle(CompanyDeleteRequest request, CancellationToken cancellationToken)
        {
            var company = await _appDbContext.Companies.FirstOrDefaultAsync(_ => _.Id == request.Id, cancellationToken);

            if (company is null) return new ResponseModel()
            {
                StatusCode = HttpStatusCode.OK,
                Message = AppMessageConstants.COMPANY_DELETED_SUCCESSFULLY
            };

            // soft delete company
            company.IsDeleted = true;
            company.DeletedBy = UserLoggedEmail;
            company.DeletedOn = DateTime.UtcNow;

            await _appDbContext.SaveChangesAsync(cancellationToken);

            return new ResponseModel
            {
                StatusCode = HttpStatusCode.OK,
                Message = AppMessageConstants.COMPANY_DELETED_SUCCESSFULLY,
            };
        }
    }
}
