using AutoMapper;
using AutoMapper.QueryableExtensions;
using ThreeSixty.Common;
using ThreeSixty.Common.Exceptions;
using ThreeSixty.Data;
using ThreeSixty.Data.Context;
using ThreeSixty.Dto;
using ThreeSixty.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;

namespace ThreeSixty.Services.Implementation
{
    public class EntityService : IEntityService
    {
        private readonly ThreeSixtyContext _threeSixtyContext;
        private readonly IMapper _mapper;
        private readonly IIncidentService _incidentService;
        private readonly AppSetting _appSetting;

        public EntityService(ThreeSixtyContext threeSixtyContext, 
                                IMapper mapper, 
                                IIncidentService incidentService,
                                IOptions<AppSetting> options)
        {
            _threeSixtyContext = threeSixtyContext;
            _mapper = mapper;
            _incidentService = incidentService;
            _appSetting = options.Value;
        }


        public async Task<IEnumerable<EntityDto>> GetEntities()
        {
            return await _threeSixtyContext.Entities.ProjectTo<EntityDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<IList<EntityDto>> GetEntitiesByName(string lastName,
            CancellationToken cancellationToken)
        {
            return await _threeSixtyContext.Entities
                .Where(r => string.IsNullOrEmpty(lastName) || (r.LastNane != null && r.LastNane.Contains(lastName)))
                .ProjectTo<EntityDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        }



        public async Task<EntityDto> GetEntity(long id, CancellationToken cancellationToken)
        {
            var entity = await _threeSixtyContext.Entities
                .Where(x => x.Id == id)
                .ProjectTo<EntityDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            return entity!;
        }

        public async Task<bool> UpdateEntity(long id, EntityDto entityDtoDto, CancellationToken cancellationToken)
        {
            var entity = await _threeSixtyContext.Entities.FindAsync(id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Entity), id);
            }

            _mapper.Map(entityDtoDto, entity);

            entity.LastModifiedDate = DateTime.Now;

            _threeSixtyContext.Entry(entity).State = EntityState.Modified;

            try
            {
                await _threeSixtyContext.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch (DbUpdateConcurrencyException exception)
            {
                Log.Error(exception, $"Could not update entityDto. Details: {JsonConvert.SerializeObject(entity)}");
                if (!EntityExists(id))
                {
                    return false;
                }

                throw;
            }
        }

        public async Task<EntityDto> AddEntity(EntityDto entityDto, CancellationToken cancellationToken)
        {
            var activeUser = System.Reflection.MethodBase.GetCurrentMethod()?.Name;

            entityDto.CreatedBy = activeUser;
            entityDto.LastModifiedBy = activeUser;
            entityDto.CreatedDate = DateTime.Now;
            entityDto.LastModifiedDate = DateTime.Now;

            var entity = _mapper.Map<Entity>(entityDto);
            _threeSixtyContext.Entities.Add(entity);
            try
            {
                await _threeSixtyContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException exception)
            {
                Log.Error(exception, $"Could not add entityDto. Details: {JsonConvert.SerializeObject(entity)}");
                if (EntityExists(entity.Id))
                {
                    return null!;
                }

                throw;
            }

            entityDto.Id = entity.Id;   
            return entityDto;
        }

        public async Task<bool> DeleteEntity(long id, CancellationToken cancellationToken)
        {
            var entity = await _threeSixtyContext.Entities
                .Where(l => l.Id == id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Entity), id);
            }

            try
            {
                _threeSixtyContext.Entities.Remove(entity);
                await _threeSixtyContext.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException exception)
            {
                Log.Error(exception, $"Could not delete entityDto. Details: {JsonConvert.SerializeObject(entity)}");
                return false;
            }

            return true;
        }


        private bool EntityExists(long id)
        {
            return _threeSixtyContext.Entities.Any(e => e.Id == id);
        }
    }
}
