using AutoMapper;
using ThreeSixty.Common;
using ThreeSixty.Dto;
using ThreeSixty.Services.Interface;
using ThreeSixty.Services.Interface.Common;

namespace ThreeSixty.Application.Incident.Commands;

public class CreateIncidentCommandHandler : IRequestHandlerWrapper<CreateIncidentCommand, Dto.IncidentDto>
{
    private readonly IMapper _mapper;
    private readonly IIncidentService _incidentService;

    public CreateIncidentCommandHandler(IIncidentService incidentService, IMapper mapper)
    {
        _mapper = mapper;
        _incidentService = incidentService;
    }

    public async Task<ServiceResult<IncidentDto>> Handle(CreateIncidentCommand request, CancellationToken cancellationToken)
    {
        var incidentDto = _mapper.Map<IncidentDto>(request);

        await _incidentService.AddIncident(incidentDto, cancellationToken);

        return ServiceResult.Success(_mapper.Map<IncidentDto>(incidentDto));
    }
}