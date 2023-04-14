using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oplog.Api.Models;
using Oplog.Core.Commands;
using Oplog.Core.Infrastructure;

namespace Oplog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LogsController : ControllerBase
    {

        private readonly ICommandDispatcher _commandDispatcher;
        public LogsController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        //TODO: do model validation
        [HttpPost]
        public async Task<IActionResult> Post(CreateLogRequest request)
        {
            await _commandDispatcher.Dispatch(new CreateLogCommand(request.LogType, request.SubType, request.Comment, request.OperationsAreaId, request.Author, request.Unit, request.EffectiveTime, GetUserName(),request.IsCritical));
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateLogRequest request)
        {
            await _commandDispatcher.Dispatch(new UpdateLogCommand(request.LogType, request.SubType, request.Comment, request.OperationsAreaId, request.Author, request.Unit, request.EffectiveTime, GetUserName(), request.IsCritical));
            return Ok();
        }

        private string GetUserName()
        {
            return HttpContext.User.Identity.Name;
        }
    }
}
