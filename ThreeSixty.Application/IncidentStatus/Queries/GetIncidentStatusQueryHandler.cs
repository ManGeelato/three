using AutoMapper;
using ThreeSixty.Application.IncidentStatus.Queries;
using ThreeSixty.Common;
using ThreeSixty.Data;
using ThreeSixty.Dto;
using ThreeSixty.Services.Interface;
using ThreeSixty.Services.Interface.Common;

namespace ThreeSixty.Application.IncidentStatus.Queries;

public class GetIncidentStatusQueryHandler : IRequestHandlerWrapper<GetAllIncidentStatusQuery, List<IncidentStatusDto>>
{
    private readonly IMapper _mapper;
    private readonly IIncidentStatusService _incidentStatusService;

    public GetIncidentStatusQueryHandler(IIncidentStatusService incidentStatusService, IMapper mapper)
    {
        _incidentStatusService = incidentStatusService;
        _mapper = mapper;
    }

    public async Task<ServiceResult<List<IncidentStatusDto>>> Handle(GetAllIncidentStatusQuery request, CancellationToken cancellationToken)
    {
        var list = (await _incidentStatusService.GetIncidentStatuses()).ToList();

        return ServiceResult.Success(list);
    }
}