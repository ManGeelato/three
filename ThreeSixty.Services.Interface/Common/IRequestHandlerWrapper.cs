using MediatR;
using ThreeSixty.Common;

namespace ThreeSixty.Services.Interface.Common;

public interface IRequestHandlerWrapper<TIn, TOut> : IRequestHandler<TIn, ServiceResult<TOut>> where TIn : IRequestWrapper<TOut>
{

}