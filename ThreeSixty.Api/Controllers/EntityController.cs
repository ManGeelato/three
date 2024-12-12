using ThreeSixty.Application.Entity.Commands;
using ThreeSixty.Application.Entity.Queries;
using ThreeSixty.Common;
using ThreeSixty.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;

namespace ThreeSixty.Api.Controllers
{
    /// <summary>
    /// Entities
    /// </summary>
    [Route("api")]
    [ApiController]
    public class EntityController : BaseApiController
    {
        /// <summary>
        /// Get all entitys
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("entities/getAll")]
        public async Task<ActionResult<ServiceResult<List<EntityDto>>>> GetAllEntities(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetAllEntitiesQuery(), cancellationToken));
        }

        /// <summary>
        /// Search entitys
        /// </summary>
        /// <param name="lastName"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("Search/{lastName}")]
        public async Task<ActionResult<ServiceResult<List<EntityDto>>>> SearchEntitiesByName(string lastName, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new SearchEntitiesQuery() { LastName = lastName }, cancellationToken));
        }

        /// <summary>
        /// Get entity by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResult<EntityDto>>> GetEntityById(int id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetEntityByIdQuery { EntityId = id }, cancellationToken));
        }

        /// <summary>
        /// Create entity
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("entities/add")]
        public async Task<ActionResult<ServiceResult<EntityDto>>> Create(CreateEntityCommand command, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(command, cancellationToken));
        }

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResult<EntityDto>>> Update(UpdateEntityCommand command, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(command, cancellationToken));
        }

        /// <summary>
        /// Delete entity by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResult<EntityDto>>> Delete(int id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new DeleteEntityCommand { Id = id }, cancellationToken));
        }
    }
}
