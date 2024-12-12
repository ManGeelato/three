using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeSixty.Common;
using ThreeSixty.Dto;
using ThreeSixty.Services.Interface;
using ThreeSixty.Services.Interface.Common;

namespace ThreeSixty.Application.Dashboard.Queries
{
    public class GetDashboardQueryHandler : IRequestHandlerWrapper<GetDashboardQuery, DashboardDto>
    {
        private readonly IMapper _mapper;
        private readonly IDashboardService _dashboardService;

        public GetDashboardQueryHandler(IDashboardService dashboardService, IMapper mapper)
        {
            _dashboardService = dashboardService;
            _mapper = mapper;
        }

        public async Task<ServiceResult<DashboardDto>> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
        {
            var list = await _dashboardService.GetDashboard();

            return ServiceResult.Success(list);
        }
    }
}
