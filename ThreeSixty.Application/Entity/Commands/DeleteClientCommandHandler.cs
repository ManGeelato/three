using AutoMapper;
using ThreeSixty.Common;
using ThreeSixty.Dto;
using ThreeSixty.Services.Interface;
using ThreeSixty.Services.Interface.Common;

namespace ThreeSixty.Application.Entity.Commands;

public class DeleteClientCommandHandler : IRequestHandlerWrapper<DeleteEntityCommand, Dto.EntityDto>
{
    private readonly IMapper _mapper;
    private readonly IEntityService _entityService;

    public DeleteClientCommandHandler(IEntityService entityService, IMapper mapper)
    {
        _entityService = entityService;
        _mapper = mapper;
    }

    public async Task<ServiceResult<Dto.EntityDto>> Handle(DeleteEntityCommand request, CancellationToken cancellationToken)
    {
        var isSuccessful = await _entityService.DeleteEntity(request.Id, cancellationToken);

        var entity = _mapper.Map<EntityDto>(request);

        return isSuccessful ? ServiceResult.Success((entity)) : ServiceResult.Failed<EntityDto>(ServiceError.DefaultError);

    }
}