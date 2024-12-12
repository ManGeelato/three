using ThreeSixty.Common;
using Microsoft.AspNetCore.Mvc;
using ThreeSixty.Application.Entity.Queries;
using ThreeSixty.Data;
using ThreeSixty.Application.IncidentStatus.Queries;

namespace ThreeSixty.Api.Controllers
{
    /// <summary>
    /// Suburbs
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentStatusController : BaseApiController
    {
        /// <summary>
        /// Get all suburbss
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ServiceResult<List<IncidentStatusDto>>>> GetIncidentStatuses(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetAllIncidentStatusQuery(), cancellationToken));
        }
    }
}