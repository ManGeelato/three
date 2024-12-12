using AutoMapper;
using AutoMapper.QueryableExtensions;
using ThreeSixty.Common.Exceptions;
using ThreeSixty.Data;
using ThreeSixty.Data.Context;
using ThreeSixty.Dto;
using ThreeSixty.Services.Interface;
using ThreeSixty.Common;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;
using System.Threading;

namespace ThreeSixty.Services.Implementation
{
    public class SuburbService : ISuburbService
    {
        private readonly ThreeSixtyContext _threeSixtyContext;
        private readonly IMapper _mapper;
        public SuburbService(ThreeSixtyContext threeSixtyContext, IMapper mapper)
        {
            _threeSixtyContext = threeSixtyContext;
            _mapper = mapper;
        }


        public async Task<IEnumerable<SuburbDto>> GetSuburbs()
        {
            return await _threeSixtyContext.Suburbs
                                            .ProjectTo<SuburbDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<IList<SuburbDto>> GetSuburbsByName(string name,
            CancellationToken cancellationToken)
        {
            return await _threeSixtyContext.Suburbs
                .Where(r => (r.Name != null && r.Name.Contains(name)) || string.IsNullOrEmpty(name))
                .ProjectTo<SuburbDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }
    }
}
