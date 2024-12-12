using AutoMapper;
using ThreeSixty.Common;
using ThreeSixty.Dto;
using ThreeSixty.Services.Interface;
using ThreeSixty.Services.Interface.Common;

namespace ThreeSixty.Application.Incident.Queries;

public class GetIncidentByIdQueryHandler : IRequestHandlerWrapper<GetIncidentByIdQuery, IncidentDto>
{
    private readonly IMapper _mapper;
    private readonly IIncidentService _incidentService;

    public GetIncidentByIdQueryHandler(IIncidentService incidentService, IMapper mapper)
    {
        _mapper = mapper;
        _incidentService = incidentService;
    }

    public async Task<ServiceResult<IncidentDto>> Handle(GetIncidentByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _incidentService.GetIncident(request.IncidentId, cancellationToken);

        return entity != null ? ServiceResult.Success(entity) : ServiceResult.Failed<IncidentDto>(ServiceError.NotFound);
    }
}