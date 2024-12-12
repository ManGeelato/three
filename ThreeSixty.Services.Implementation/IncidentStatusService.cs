using ThreeSixty.Services.Interface;
using ThreeSixty.Common;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;
using ThreeSixty.Data;
using ThreeSixty.Data.Context;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace ThreeSixty.Services.Implementation
{
    public class IncidentStatusService : IIncidentStatusService
    {
        private readonly ThreeSixtyContext _threeSixtyContext;
        private readonly IMapper _mapper;
        public IncidentStatusService(ThreeSixtyContext threeSixtyContext, IMapper mapper)
        {
            _threeSixtyContext = threeSixtyContext;
            _mapper = mapper;
        }


        public async Task<IEnumerable<IncidentStatusDto>> GetIncidentStatuses()
        {

            return await _threeSixtyContext.IncidentStatuses.ProjectTo<IncidentStatusDto>(_mapper.ConfigurationProvider).ToListAsync();
        }


    }
}

