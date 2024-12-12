using AutoMapper;
using ThreeSixty.Common;
using ThreeSixty.Dto;
using ThreeSixty.Services.Interface;
using ThreeSixty.Services.Interface.Common;

namespace ThreeSixty.Application.Entity.Commands;

public class CreateEntityCommandHandler : IRequestHandlerWrapper<CreateEntityCommand, Dto.EntityDto>
{
    private readonly IMapper _mapper;
    private readonly IEntityService _entityService;

    public CreateEntityCommandHandler(IEntityService entityService, IMapper mapper)
    {
        _mapper = mapper;
        _entityService = entityService;
    }

    public async Task<ServiceResult<EntityDto>> Handle(CreateEntityCommand request, CancellationToken cancellationToken)
    {
        var entityDto = _mapper.Map<EntityDto>(request);

        await _entityService.AddEntity(entityDto, cancellationToken);

        return ServiceResult.Success(_mapper.Map<EntityDto>(entityDto));
    }
}