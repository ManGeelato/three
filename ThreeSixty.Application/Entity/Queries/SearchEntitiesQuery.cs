using ThreeSixty.Dto;
using ThreeSixty.Services.Interface.Common;

namespace ThreeSixty.Application.Entity.Queries
{
    public class SearchEntitiesQuery : IRequestWrapper<List<EntityDto>>
    {
        public string LastName { get; set; }
    }
}
