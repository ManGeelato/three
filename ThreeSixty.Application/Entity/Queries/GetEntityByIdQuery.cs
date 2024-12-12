using ThreeSixty.Dto;
using ThreeSixty.Services.Interface.Common;

namespace ThreeSixty.Application.Entity.Queries
{
    public class GetEntityByIdQuery : IRequestWrapper<EntityDto>
    {
        public int EntityId { get; set; }
    }
}
