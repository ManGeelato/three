using ThreeSixty.Dto;

namespace ThreeSixty.Services.Interface
{
    public interface IEntityService
    {
        Task<IEnumerable<EntityDto>> GetEntities();

        Task<IList<EntityDto>> GetEntitiesByName(string lastName,
            CancellationToken cancellationToken);
        Task<EntityDto> GetEntity(long id, CancellationToken cancellationToken);

        Task<bool> UpdateEntity(long id, EntityDto entityDto, CancellationToken cancellationToken);

        Task<EntityDto> AddEntity(EntityDto entityDto, CancellationToken cancellationToken);

        Task<bool> DeleteEntity(long id, CancellationToken cancellationToken);
    }
}