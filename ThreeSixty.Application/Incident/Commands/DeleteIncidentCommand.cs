using ThreeSixty.Services.Interface.Common;

namespace ThreeSixty.Application.Incident.Commands
{
    public class DeleteIncidentCommand : IRequestWrapper<Dto.IncidentDto>
    {
        public int Id { get; set; }
    }
}
