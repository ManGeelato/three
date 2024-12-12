using ThreeSixty.Dto;
using ThreeSixty.Services.Interface.Common;

namespace ThreeSixty.Application.Suburb.Queries
{
    public class SearchSuburbsQuery : IRequestWrapper<List<SuburbDto>>
    {
        public string Name { get; set; }
    }
}
