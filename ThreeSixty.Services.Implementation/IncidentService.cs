using AutoMapper;
using AutoMapper.QueryableExtensions;
using ThreeSixty.Common.Exceptions;
using ThreeSixty.Data;
using ThreeSixty.Data.Context;
using ThreeSixty.Dto;
using ThreeSixty.Services.Interface;
using ThreeSixty.Common;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Serilog;

namespace ThreeSixty.Services.Implementation
{
    public class IncidentService : IIncidentService
    {
        private readonly ThreeSixtyContext _threeSixtyContext;
        private readonly IMapper _mapper;
        public IncidentService(ThreeSixtyContext threeSixtyContext, IMapper mapper)
        {
            _threeSixtyContext = threeSixtyContext;
            _mapper = mapper;
        }


        public async Task<IEnumerable<IncidentDto>> GetIncidents()
        {
            var incidents = await _threeSixtyContext.Incidents
                .Include(r => r.IncidentType)
                .Include(r => r.IncidentStatus).ToListAsync();
            var query = from incident in incidents

                        select new IncidentDto
                        {
                            Id = incident.Id,
                            IncidentTypeId = incident.IncidentTypeId,
                            IncidentTypeName = incident.IncidentType.Name,
                            IncidentStatusId = (int)incident.IncidentStatusId,
                            IncidentStatusName = incident.IncidentStatus.Name,
                            EntityId = incident.EntityId,
                            ShortDescription =incident.ShortDescription,
                            LongDescription = incident.LongDescription,
                            IncidentDate = incident.IncidentDate,
                        };

            var entities = await _threeSixtyContext.Entities.ToListAsync();
            var entitiesResults = entities
                         .Join(query,
                                 p => p.Id,
                                 s => s.EntityId,
                                 (p, s) => new IncidentDto()
                                 {
                                  Id = s.Id,
                                  FirstName = p.FirstName,
                                  LastNane = p.LastNane,
                                  Address = p.Address,
                                  ShortDescription = s.ShortDescription,
                                  LongDescription = s.LongDescription,
                                  CreatedBy = p.CreatedBy,
                                  CreatedDate = p.CreatedDate,
                                  EntityId = p.Id,
                                  IncidentDate = s.IncidentDate,
                                  IncidentTypeId = s.IncidentTypeId,
                                  IncidentTypeName = s.IncidentTypeName,
                                  LastModifiedBy = s.LastModifiedBy,
                                  LastModifiedDate = s.LastModifiedDate,
                                  IncidentStatusId = s.IncidentStatusId,
                                  IncidentStatusName= s.IncidentStatusName
                                 });


            return entitiesResults.ToList();
        }

        public async Task<IEnumerable<IncidentDto>> GetIncidentsList()
        {
            return await _threeSixtyContext.Incidents.ProjectTo<IncidentDto>(_mapper.ConfigurationProvider).ToListAsync();
        }


        public async Task<IList<IncidentDto>> GetIncidentsByDate(DateTime incidentDate,
            CancellationToken cancellationToken)
        {
            return await _threeSixtyContext.Incidents
                .Where(r => r.CreatedDate.Date >= incidentDate.Date)
                .ProjectTo<IncidentDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }

        public async Task<IncidentDto> GetIncident(long id, CancellationToken cancellationToken)
        {
            var entity = await _threeSixtyContext.Incidents
                .Where(x => x.Id == id)
                .ProjectTo<IncidentDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            return entity!;
        }

        public async Task<bool> UpdateIncident(long id, IncidentDto incidentDtoDto, CancellationToken cancellationToken)
        {
            var incident = await _threeSixtyContext.Incidents.FindAsync(id);

            if (incident == null)
            {
                throw new NotFoundException(nameof(Incident), id);
            }

            _mapper.Map(incidentDtoDto, incident);

            incident.LastModifiedDate = DateTime.Now;

            _threeSixtyContext.Entry(incident).State = EntityState.Modified;

            try
            {
                await _threeSixtyContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                Log.Error(exception, $"Could not update incidentDto. Details: {JsonConvert.SerializeObject(incident)}");
                if (!IncidentExists(id))
                {
                    return false;
                }

                throw;
            }
        }

        public async Task<IncidentDto> AddIncident(IncidentDto incidentDto, CancellationToken cancellationToken)
        {
            var incident = _mapper.Map<Incident>(incidentDto);

            incident.CreatedBy = incidentDto.CreatedBy ?? "System";
            incident.LastModifiedBy = incidentDto.LastModifiedBy ?? "System";
            incident.CreatedDate = DateTime.Now;
            incident.LastModifiedDate = DateTime.Now;
            incident.IncidentDate = incidentDto.IncidentDate ?? DateTime.Now;
            incident.IncidentStatusId = (int)Enums.IncidentStatus.Pending;
            incident.IncidentStatusReasonId = (int)Enums.IncidentStatusReason.Pending;
            incident.IncidentTypeId = incidentDto.IncidentTypeId;

            incident.IncidentType = null;

            _threeSixtyContext.Incidents.Add(incident);
            try
            {
                await _threeSixtyContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException exception)
            {
                Log.Error(exception, $"Could not add incidentDto. Details: {JsonConvert.SerializeObject(incident)}");
                if (IncidentExists(incident.Id))
                {
                    return null!;
                }

                throw;
            }

            return incidentDto;
        }

        public async Task<bool> DeleteIncident(long id, CancellationToken cancellationToken)
        {
            var incident = await _threeSixtyContext.Incidents
                .Where(l => l.Id == id)
                .SingleOrDefaultAsync(cancellationToken);

            if (incident == null)
            {
                throw new NotFoundException(nameof(Incident), id);
            }

            try
            {
                _threeSixtyContext.Incidents.Remove(incident);
                await _threeSixtyContext.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                Log.Error(exception, $"Could not delete incidentDto. Details: {JsonConvert.SerializeObject(incident)}");
                return false;
            }

            return true;
        }

        private bool IncidentExists(long id)
        {
            return _threeSixtyContext.Incidents.Any(e => e.Id == id);
        }
    }
}
