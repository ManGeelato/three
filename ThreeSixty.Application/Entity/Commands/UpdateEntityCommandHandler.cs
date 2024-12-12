    using AutoMapper;
using ThreeSixty.Common;
using ThreeSixty.Dto;
using ThreeSixty.Services.Interface;
using ThreeSixty.Services.Interface.Common;

namespace ThreeSixty.Application.Entity.Commands;

public class UpdateEntityCommandHandler : IRequestHandlerWrapper<UpdateEntityCommand, EntityDto>
{
    private readonly IMapper _mapper;
    private readonly IEntityService _entityService;

    public UpdateEntityCommandHandler(IEntityService entityService, IMapper mapper)
    {
        _entityService = entityService;
        _mapper = mapper;
    }

    public async Task<ServiceResult<EntityDto>> Handle(UpdateEntityCommand request, CancellationToken cancellationToken)
    {
        var entityDto = _mapper.Map<EntityDto>(request);
        await _entityService.UpdateEntity(request.Id, entityDto, cancellationToken);

        return ServiceResult.Success(entityDto);
    }
}