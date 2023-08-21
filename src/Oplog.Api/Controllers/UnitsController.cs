using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oplog.Core.Queries;
using System.Threading.Tasks;

namespace Oplog.Api.Controllers
{
    [Route("api/units")]
    [ApiController]
    [Authorize]
    public class UnitsController : ControllerBase
    {
        private readonly IConfiguredTypesQueries _configuredTypesQueries;
        public UnitsController(IConfiguredTypesQueries configuredTypesQueries)
        {
            _configuredTypesQueries = configuredTypesQueries;
        }

        [HttpGet("{area}")]
        public async Task<IActionResult> Get(string area)
        {
            var results = await _configuredTypesQueries.GetUnitsByAreaName(area);
            if (results == null)
            {
                return NotFound("no units found");
            }

            return Ok(results);
        }
    }
}
