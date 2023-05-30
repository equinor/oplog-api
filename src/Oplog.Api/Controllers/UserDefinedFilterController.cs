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
    public class UserDefinedFilterController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IUserDefinedFilterQueries _userDefinedFilterQueries;
        public UserDefinedFilterController(ICommandDispatcher commandDispatcher, IUserDefinedFilterQueries userDefinedFilterQueries)
        {
            _commandDispatcher = commandDispatcher;
            _userDefinedFilterQueries = userDefinedFilterQueries;
        }


        [HttpPost]
        public async Task<IActionResult> Post(CreateUserDefinedFilterRequest request)
        {
            await _commandDispatcher.Dispatch(new CreateUserDefinedFilterCommand(request.Name, HttpContext.User.Identity.Name, request.FilterItems));
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var results = await _userDefinedFilterQueries.GetByCreatedUser(HttpContext.User.Identity.Name);
            return Ok(results);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            await _commandDispatcher.Dispatch(new DeleteUserDefinedFilterCommand(id));
            return Ok();
        }
    }
}
