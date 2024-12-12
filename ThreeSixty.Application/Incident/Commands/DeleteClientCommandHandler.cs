using AutoMapper;
using ThreeSixty.Common;
using ThreeSixty.Dto;
using ThreeSixty.Services.Interface;
using ThreeSixty.Services.Interface.Common;

namespace ThreeSixty.Application.Incident.Commands;

public class DeleteClientCommandHandler : IRequestHandlerWrapper<DeleteIncidentCommand, Dto.IncidentDto>
{
    private readonly IMapper _mapper;
    private readonly IIncidentService _incidentService;

    public DeleteClientCommandHandler(IIncidentService incidentService, IMapper mapper)
    {
        _incidentService = incidentService;
        _mapper = mapper;
    }

    public async Task<ServiceResult<Dto.IncidentDto>> Handle(DeleteIncidentCommand request, CancellationToken cancellationToken)
    {
        var isSuccessful = await _incidentService.DeleteIncident(request.Id, cancellationToken);

        var entity = _mapper.Map<IncidentDto>(request);

        return isSuccessful ? ServiceResult.Success((entity)) : ServiceResult.Failed<IncidentDto>(ServiceError.DefaultError);

    }
}