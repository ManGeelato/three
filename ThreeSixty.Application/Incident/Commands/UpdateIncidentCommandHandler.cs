using AutoMapper;
using ThreeSixty.Common;
using ThreeSixty.Dto;
using ThreeSixty.Services.Interface;
using ThreeSixty.Services.Interface.Common;

namespace ThreeSixty.Application.Incident.Commands;

public class UpdateIncidentCommandHandler : IRequestHandlerWrapper<UpdateIncidentCommand, IncidentDto>
{
    private readonly IMapper _mapper;
    private readonly IIncidentService _incidentService;

    public UpdateIncidentCommandHandler(IIncidentService incidentService, IMapper mapper)
    {
        _incidentService = incidentService;
        _mapper = mapper;
    }

    public async Task<ServiceResult<IncidentDto>> Handle(UpdateIncidentCommand request, CancellationToken cancellationToken)
    {
        var incidentDto = _mapper.Map<IncidentDto>(request);
        await _incidentService.UpdateIncident(request.Id, incidentDto, cancellationToken);

        return ServiceResult.Success(incidentDto);
    }
}