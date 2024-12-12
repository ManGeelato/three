using AutoMapper;
using ThreeSixty.Common;
using ThreeSixty.Dto;
using ThreeSixty.Services.Interface;
using ThreeSixty.Services.Interface.Common;

namespace ThreeSixty.Application.Suburb.Queries;

public class GetSuburbsQueryHandler : IRequestHandlerWrapper<GetAllSuburbsQuery, List<SuburbDto>>
{
    private readonly IMapper _mapper;
    private readonly ISuburbService _suburbService;

    public GetSuburbsQueryHandler(ISuburbService suburbService, IMapper mapper)
    {
        _suburbService = suburbService;
        _mapper = mapper;
    }

    public async Task<ServiceResult<List<SuburbDto>>> Handle(GetAllSuburbsQuery request, CancellationToken cancellationToken)
    {
        var list = (await _suburbService.GetSuburbs()).ToList();

        return ServiceResult.Success(list);
    }
}