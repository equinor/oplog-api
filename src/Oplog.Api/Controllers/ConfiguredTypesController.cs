using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oplog.Core.Enums;
using Oplog.Core.Queries;
using System.Threading.Tasks;

namespace Oplog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConfiguredTypesController : ControllerBase
    {
        private readonly IConfiguredTypesQueries _configuredTypesQueries;
        public ConfiguredTypesController(IConfiguredTypesQueries configuredTypesQueries)
        {
            _configuredTypesQueries = configuredTypesQueries;
        }

        [HttpGet("{categoryName}")]
        public async Task<IActionResult> Get(string categoryName)
        {
            var categoryId = ExtractCategoryId(categoryName);

            if (categoryId == null)
            {
                return BadRequest($"Incorrect category name: {categoryName}");
            }

            var configuredTypes = await _configuredTypesQueries.GetConfiguredTypesByCategory(categoryId.Value);

            if (configuredTypes == null)
            {
                return NotFound("no configured types found");
            }

            return Ok(configuredTypes);
        }

        [HttpGet("grouped")]
        public async Task<IActionResult> GetAllGrouped()
        {
            var result = await _configuredTypesQueries.GetAllGrouped();
            return Ok(result);
        }

        private CategoryId? ExtractCategoryId(string categoryName)
        {
            switch (categoryName)
            {
                case "type":
                    return CategoryId.Type;
                case "subtype":
                    return CategoryId.SubType;
                case "unit":
                    return CategoryId.Unit;

                default:
                    return null;
            }
        }
    }
}
