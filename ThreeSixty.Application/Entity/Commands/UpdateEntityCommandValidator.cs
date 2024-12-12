using FluentValidation;
using ThreeSixty.Data.Context;

namespace ThreeSixty.Application.Entity.Commands
{
    public class UpdateEntityCommandValidator : AbstractValidator<UpdateEntityCommand>
    {
        private readonly ThreeSixtyContext _context;
        public UpdateEntityCommandValidator(ThreeSixtyContext context)
        {
            _context = context;
            RuleFor(v => v.Id).NotNull();
        }
    }
}
