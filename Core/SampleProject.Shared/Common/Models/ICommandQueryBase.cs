using MediatR;

namespace SampleProject.Shared.Common.Models
{
    public interface ICommandQueryBase : IRequest<ResponseModel>
    {
    }
}
