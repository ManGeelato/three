using MediatR;
using ThreeSixty.Common;

namespace ThreeSixty.Services.Interface.Common
{
    public interface IRequestWrapper<T> : IRequest<ServiceResult<T>>
    {

    }
}
