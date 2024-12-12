using AutoMapper;
using ThreeSixty.Common;
using ThreeSixty.Dto;
using ThreeSixty.Services.Interface;
using ThreeSixty.Services.Interface.Common;

namespace ThreeSixty.Application.Entity.Queries;

public class GetEntitiesQueryHandler : IRequestHandlerWrapper<GetAllEntitiesQuery, List<EntityDto>>
{
    private readonly IMapper _mapper;
    private readonly IEntityService _entityService;

    public GetEntitiesQueryHandler(IEntityService entityService, IMapper mapper)
    {
        _entityService = entityService;
        _mapper = mapper;
    }

    public async Task<ServiceResult<List<EntityDto>>> Handle(GetAllEntitiesQuery request, CancellationToken cancellationToken)
    {
        var list = (await _entityService.GetEntities()).ToList();

        return ServiceResult.Success(list);
    }
}