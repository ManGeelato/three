using ThreeSixty.Data;
using ThreeSixty.Dto;

namespace ThreeSixty.Services.Interface
{
    public interface IIncidentStatusService
    {
        Task<IEnumerable<IncidentStatusDto>> GetIncidentStatuses();
    }
}
