using AutoMapper;
using ThreeSixty.Common;
using ThreeSixty.Dto;
using ThreeSixty.Services.Interface;
using ThreeSixty.Services.Interface.Common;

namespace ThreeSixty.Application.Entity.Queries;

public class SearchEntitiesQueryHandler : IRequestHandlerWrapper<SearchEntitiesQuery, List<EntityDto>>
{
    private readonly IMapper _mapper;
    private readonly IEntityService _entityService;

    public SearchEntitiesQueryHandler(IEntityService entityService, IMapper mapper)
    {
        _entityService = entityService;
        _mapper = mapper;
    }

    public async Task<ServiceResult<List<EntityDto>>> Handle(SearchEntitiesQuery request, CancellationToken cancellationToken)
    {
        var list = (await _entityService.GetEntitiesByName(request.LastName, cancellationToken)).ToList();

        return ServiceResult.Success(list);
    }
}