using ThreeSixty.Dto;
using ThreeSixty.Services.Interface.Common;

namespace ThreeSixty.Application.Incident.Queries
{
    public class GetIncidentByIdQuery : IRequestWrapper<IncidentDto>
    {
        public int IncidentId { get; set; }
    }
}
