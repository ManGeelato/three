using ThreeSixty.Dto;

namespace ThreeSixty.Services.Interface
{
    public interface ISuburbService
    {
        Task<IEnumerable<SuburbDto>> GetSuburbs();

        Task<IList<SuburbDto>> GetSuburbsByName(string name,
            CancellationToken cancellationToken);
    }
}