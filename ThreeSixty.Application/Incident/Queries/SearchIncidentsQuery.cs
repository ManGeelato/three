using ThreeSixty.Dto;
using ThreeSixty.Services.Interface.Common;

namespace ThreeSixty.Application.Incident.Queries
{
    public class SearchIncidentsQuery : IRequestWrapper<List<IncidentDto>>
    {
        public DateTime IncidentDate { get; set; }
    }
}
