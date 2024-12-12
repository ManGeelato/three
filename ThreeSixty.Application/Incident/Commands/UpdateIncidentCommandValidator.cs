using FluentValidation;
using ThreeSixty.Data.Context;

namespace ThreeSixty.Application.Incident.Commands
{
    public class UpdateIncidentCommandValidator : AbstractValidator<UpdateIncidentCommand>
    {
        private readonly ThreeSixtyContext _context;
        public UpdateIncidentCommandValidator(ThreeSixtyContext context)
        {
            _context = context;

            RuleFor(v => v.Id).NotNull();
        }
    }
}
