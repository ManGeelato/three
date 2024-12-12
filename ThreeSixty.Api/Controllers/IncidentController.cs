using ThreeSixty.Common;
using ThreeSixty.Dto;
using Microsoft.AspNetCore.Mvc;
using ThreeSixty.Application.Incident.Commands;
using ThreeSixty.Application.Incident.Queries;

namespace ThreeSixty.Api.Controllers
{
    /// <summary>
    /// Incidents
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentController : BaseApiController
    {
        /// <summary>
        /// Get all incidents
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("getAll")]
        public async Task<ActionResult<ServiceResult<List<IncidentDto>>>> GetAllIncidents(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetAllIncidentsQuery(), cancellationToken));
        }

        /// <summary>
        /// Search incidents
        /// </summary>
        /// <param name="incidentDate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("Search/{incidentDate}")]
        public async Task<ActionResult<ServiceResult<List<IncidentDto>>>> SearchIncidents(DateTime incidentDate, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new SearchIncidentsQuery() { IncidentDate = incidentDate }, cancellationToken));
        }

        /// <summary>
        /// Get incident by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResult<IncidentDto>>> GetIncidentById(int id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetIncidentByIdQuery { IncidentId = id }, cancellationToken));
        }

        /// <summary>
        /// Create incident
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<ActionResult<ServiceResult<IncidentDto>>> Create(CreateIncidentCommand command, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(command, cancellationToken));
        }

        /// <summary>
        /// Update incident
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResult<IncidentDto>>> Update(UpdateIncidentCommand command, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(command, cancellationToken));
        }

        /// <summary>
        /// Delete incident by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResult<IncidentDto>>> Delete(int id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new DeleteIncidentCommand { Id = id }, cancellationToken));
        }
    }
}
