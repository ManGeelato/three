using AutoMapper;
using ThreeSixty.Common;
using ThreeSixty.Dto;
using ThreeSixty.Services.Interface;
using ThreeSixty.Services.Interface.Common;

namespace ThreeSixty.Application.Incident.Queries;

public class SearchIncidentsQueryHandler : IRequestHandlerWrapper<SearchIncidentsQuery, List<IncidentDto>>
{
    private readonly IMapper _mapper;
    private readonly IIncidentService _incidentService;

    public SearchIncidentsQueryHandler(IIncidentService incidentService, IMapper mapper)
    {
        _incidentService = incidentService;
        _mapper = mapper;
    }

    public async Task<ServiceResult<List<IncidentDto>>> Handle(SearchIncidentsQuery request, CancellationToken cancellationToken)
    {
        var list = (await _incidentService.GetIncidentsByDate(request.IncidentDate, cancellationToken)).ToList();

        return ServiceResult.Success(list);
    }
}