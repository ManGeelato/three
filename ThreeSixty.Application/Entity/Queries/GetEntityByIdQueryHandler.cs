using AutoMapper;
using ThreeSixty.Common;
using ThreeSixty.Dto;
using ThreeSixty.Services.Interface;
using ThreeSixty.Services.Interface.Common;

namespace ThreeSixty.Application.Entity.Queries;

public class GetEntityByIdQueryHandler : IRequestHandlerWrapper<GetEntityByIdQuery, EntityDto>
{
    private readonly IMapper _mapper;
    private readonly IEntityService _entityService;

    public GetEntityByIdQueryHandler(IEntityService entityService, IMapper mapper)
    {
        _mapper = mapper;
        _entityService = entityService;
    }

    public async Task<ServiceResult<EntityDto>> Handle(GetEntityByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _entityService.GetEntity(request.EntityId, cancellationToken);

        return entity != null ? ServiceResult.Success(entity) : ServiceResult.Failed<EntityDto>(ServiceError.NotFound);
    }
}