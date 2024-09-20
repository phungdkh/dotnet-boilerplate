using MediatR;
using SampleProject.Shared.Common.Models;

namespace SampleProject.Application.Features.Companies.Queries.Companies.Request
{
    public class CompanyListRequest : IRequest<ResponseModel>
    {
        public bool IsDeleted { get; set; } = false;
    }
}
