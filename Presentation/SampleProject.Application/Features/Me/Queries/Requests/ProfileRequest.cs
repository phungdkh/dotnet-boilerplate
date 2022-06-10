using MediatR;
using SampleProject.Shared.Common.Models;

namespace SampleProject.Application.Features.Me.Queries.Requests
{
    public class ProfileRequest : IRequest<ResponseModel>
    {
    }
}
