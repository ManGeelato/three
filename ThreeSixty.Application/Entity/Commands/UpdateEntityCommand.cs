using ThreeSixty.Application.Incident.Commands;
using ThreeSixty.Dto;
using ThreeSixty.Services.Interface.Common;

namespace ThreeSixty.Application.Entity.Commands
{
    public class UpdateEntityCommand : IRequestWrapper<EntityDto>
    {
        public UpdateEntityCommand()
        {
            Incidents = new List<UpdateIncidentCommand>();
        }
        public long Id { get; set; }
        public string? IdentityNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastNane { get; set; }
        public string? Address { get; set; }
        public string? EmailAddress { get; set; }
        public List<UpdateIncidentCommand> Incidents { get; set; }
    }
}
