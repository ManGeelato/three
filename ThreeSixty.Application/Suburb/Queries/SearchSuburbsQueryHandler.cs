using AutoMapper;
using ThreeSixty.Common;
using ThreeSixty.Dto;
using ThreeSixty.Services.Interface;
using ThreeSixty.Services.Interface.Common;

namespace ThreeSixty.Application.Suburb.Queries;

public class SearchSuburbsQueryHandler : IRequestHandlerWrapper<SearchSuburbsQuery, List<SuburbDto>>
{
    private readonly IMapper _mapper;
    private readonly ISuburbService _suburbsService;

    public SearchSuburbsQueryHandler(ISuburbService suburbsService, IMapper mapper)
    {
        _suburbsService = suburbsService;
        _mapper = mapper;
    }

    public async Task<ServiceResult<List<SuburbDto>>> Handle(SearchSuburbsQuery request, CancellationToken cancellationToken)
    {
        var list = (await _suburbsService.GetSuburbsByName(request.Name, cancellationToken)).ToList();

        return ServiceResult.Success(list);
    }
}