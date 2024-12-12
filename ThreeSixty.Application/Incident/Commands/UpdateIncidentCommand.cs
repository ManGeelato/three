using ThreeSixty.Dto;
using ThreeSixty.Services.Interface.Common;

namespace ThreeSixty.Application.Incident.Commands
{
    public class UpdateIncidentCommand : IRequestWrapper<IncidentDto>
    {
        public long Id { get; set; }
        public int IncidentTypeId { get; set; }
        public long EntityId { get; set; }
        public string? LastNane { get; set; }
        public string? ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public decimal? LocationXcoordinate { get; set; }
        public decimal? LocationYcoordinate { get; set; }
        public string? LocationDescription { get; set; }
        public string? LocationLatLng { get; set; }
        public DateTime? IncidentDate { get; set; }
    }
}
