using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oplog.Api.Models;
using Oplog.Core.Commands;
using Oplog.Core.Infrastructure;
using Oplog.Core.Queries;
using System.Threading.Tasks;

namespace Oplog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomFilterController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly ICustomFilterQueries _customFilterQueries;
        public CustomFilterController(ICommandDispatcher commandDispatcher, ICustomFilterQueries customFilterQueries)
        {
            _commandDispatcher = commandDispatcher;
            _customFilterQueries = customFilterQueries;
        }


        [HttpPost]
        public async Task<IActionResult> Post(CreateCustomFilterRequest request)
        {
            var isAdmin = User.IsInRole("Permission.Admin");
            var result = await _commandDispatcher.Dispatch<CreateCustomFilterCommand, CreateCustomFilterResult>(new CreateCustomFilterCommand(request.Name, HttpContext.User.Identity.Name, request.IsGlobalFilter, request.SearchText, isAdmin, request.FilterItems));

            if (result.ResultType == ResultType.NotAllowed)
            {
                return Unauthorized(result.Message);
            }
            return Ok(result.Message);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var results = await _customFilterQueries.GetByCreatedUser(HttpContext.User.Identity.Name);
            return Ok(results);
        }

        [HttpGet("global")]
        public async Task<IActionResult> GetGlobalCustomFilters()
        {
            var results = await _customFilterQueries.GetGlobalCustomFilters();
            return Ok(results);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            var isAdmin = User.IsInRole("Permission.Admin");
            var result = await _commandDispatcher.Dispatch<DeleteCustomFilterCommand, DeleteCustomFilterResult>(new DeleteCustomFilterCommand(id, isAdmin));
            if (result.ResultType == ResultType.NotFound)
            {
                return NotFound(result.Message);
            }

            return Ok(result.Message);
        }
    }
}
