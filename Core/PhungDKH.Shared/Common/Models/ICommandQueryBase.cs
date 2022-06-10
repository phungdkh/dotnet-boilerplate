using MediatR;

namespace PhungDKH.Shared.Common.Models
{
    public interface ICommandQueryBase : IRequest<ResponseModel>
    {
    }
}
