using MediatR;
using SampleProject.Shared.Common.Models;

namespace SampleProject.Application.Features.Companies.Commands.Companies.Request
{
    public class CompanyDeleteRequest : IRequest<ResponseModel>
    {
        public Guid Id { get; set; }
    }
}
