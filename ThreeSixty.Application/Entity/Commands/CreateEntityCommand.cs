using ThreeSixty.Dto;
using ThreeSixty.Services.Interface.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting.Internal;
using ThreeSixty.Application.Incident.Commands;

namespace ThreeSixty.Application.Entity.Commands
{
    public class CreateEntityCommand : IRequestWrapper<EntityDto>
    {
        public CreateEntityCommand()
        {
            Incidents = new List<CreateIncidentCommand>();
        }
        public long Id { get; set; }
        public string? IdentityNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastNane { get; set; }
        public string? Address { get; set; }
        public string? EmailAddress { get; set; }
        public List<CreateIncidentCommand> Incidents { get; set; }
    }
}
