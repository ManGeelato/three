using ThreeSixty.Common;
using ThreeSixty.Dto;
using Microsoft.AspNetCore.Mvc;
using ThreeSixty.Application.Suburb.Queries;
using ThreeSixty.Application.Dashboard.Queries;

namespace ThreeSixty.Api.Controllers
{
    /// <summary>
    /// Suburbs
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : BaseApiController
    {
        /// <summary>
        /// Get all suburbss
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("getAll")]
        public async Task<ActionResult<ServiceResult<List<DashboardDto>>>> GetDashboard(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetDashboardQuery(), cancellationToken));
        }
    }
}
