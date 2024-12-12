using ThreeSixty.Dto;

namespace ThreeSixty.Services.Interface
{
    public interface IIncidentService
    {
        Task<IEnumerable<IncidentDto>> GetIncidents();
        Task<IEnumerable<IncidentDto>> GetIncidentsList();

        Task<IList<IncidentDto>> GetIncidentsByDate(DateTime incidentDate,
            CancellationToken cancellationToken);
        Task<IncidentDto> GetIncident(long id, CancellationToken cancellationToken);

        Task<bool> UpdateIncident(long id, IncidentDto incidentDto, CancellationToken cancellationToken);

        Task<IncidentDto> AddIncident(IncidentDto incidentDto, CancellationToken cancellationToken);

        Task<bool> DeleteIncident(long id, CancellationToken cancellationToken);
    }
}