using FluentValidation;
using ThreeSixty.Services.Interface;

namespace ThreeSixty.Application.Incident.Commands
{
    public class CreateIncidentCommandValidator : AbstractValidator<CreateIncidentCommand>
    {
        private readonly IIncidentService _incidentService;

        public CreateIncidentCommandValidator(IIncidentService incidentService)
        {
            _incidentService = incidentService;
        }
    }
}
