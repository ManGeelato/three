using FluentValidation;
using ThreeSixty.Data.Context;
using ThreeSixty.Services.Interface;

namespace ThreeSixty.Application.Entity.Commands
{
    public class CreateEntityCommandValidator : AbstractValidator<CreateEntityCommand>
    {
        private readonly IEntityService _entityService;

        public CreateEntityCommandValidator(IEntityService entityService)
        {
            _entityService = entityService;
        }
    }
}
