using AutoMapper;
using AutoMapper.QueryableExtensions;
using ThreeSixty.Common;
using ThreeSixty.Common.Exceptions;
using ThreeSixty.Data;
using ThreeSixty.Data.Context;
using ThreeSixty.Dto;
using ThreeSixty.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;
using System.Collections.Generic;

namespace ThreeSixty.Services.Implementation
{
    public class DashboardService : IDashboardService
    {
        private readonly ThreeSixtyContext _threeSixtyContext;
        private readonly IMapper _mapper;
        private readonly IIncidentService _incidentService;
        private readonly AppSetting _appSetting;

        public DashboardService(ThreeSixtyContext threeSixtyContext,
                                IMapper mapper,
                                IIncidentService incidentService,
                                IOptions<AppSetting> options)
        {
            _threeSixtyContext = threeSixtyContext;
            _mapper = mapper;
            _incidentService = incidentService;
            _appSetting = options.Value;
        }


        public async Task<DashboardDto> GetDashboard()
        {

            var incidents = (await _incidentService.GetIncidentsList()).ToList();

            var numberOfIncidentToday = incidents.Where(j => j.CreatedDate.Date == DateTime.Today).Count();           

            var numberOfIncidentThisWeek = incidents.Where(j => j.CreatedDate.DayOfWeek == DateTime.Now.DayOfWeek).Count();

            var numberOfIncidentsThisMonth = incidents.Where(j => j.CreatedDate.Month == DateTime.Today.Month).Count();

            var totalNumberOfIncidentAllTime = incidents.Count();

            var dashboard =  new DashboardDto
                    {
                        NumberOfIncidentThisWeek = numberOfIncidentThisWeek,
                        NumberOfIncidentThisMonth = numberOfIncidentsThisMonth,
                        NumberOfIncidentToday = numberOfIncidentToday,
                        TotalNumberOfIncidentsAllTime = totalNumberOfIncidentAllTime
                    };

            return dashboard;
           
        }

    }
}
