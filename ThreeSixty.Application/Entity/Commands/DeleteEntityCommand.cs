using ThreeSixty.Services.Interface.Common;

namespace ThreeSixty.Application.Entity.Commands
{
    public class DeleteEntityCommand : IRequestWrapper<Dto.EntityDto>
    {
        public int Id { get; set; }
    }
}
