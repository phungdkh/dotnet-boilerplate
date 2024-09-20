using MediatR;
using SampleProject.Shared.Common.Models;

namespace SampleProject.Application.Features.Companies.Queries.Companies.Request
{
    public class CompanyGetByIdRequest : IRequest<ResponseModel>
    {
        public Guid Id { get; set; }
    }
}
