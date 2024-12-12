using AutoMapper;
using ThreeSixty.Common;
using ThreeSixty.Dto;
using ThreeSixty.Services.Interface;
using ThreeSixty.Services.Interface.Common;

namespace ThreeSixty.Application.Incident.Queries;

public class GetIncidentsQueryHandler : IRequestHandlerWrapper<GetAllIncidentsQuery, List<IncidentDto>>
{
    private readonly IMapper _mapper;
    private readonly IIncidentService _incidentService;

    public GetIncidentsQueryHandler(IIncidentService incidentService, IMapper mapper)
    {
        _incidentService = incidentService;
        _mapper = mapper;
    }

    public async Task<ServiceResult<List<IncidentDto>>> Handle(GetAllIncidentsQuery request, CancellationToken cancellationToken)
    {
        var list = (await _incidentService.GetIncidents()).ToList();

        return ServiceResult.Success(list);
    }
}