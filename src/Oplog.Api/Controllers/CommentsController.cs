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
    public class CommentsController : ControllerBase
    {

        private readonly ICommandDispatcher _commandDispatcher;
        public CommentsController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateCommentRequest request)
        {
            await _commandDispatcher.Dispatch(new CreateCommentCommand(request.LogType, request.SubType, request.Comment, request.OperationsAreaId, request.Author, request.Unit, request.EffectiveTime));
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateCommentRequest request)
        {
            await _commandDispatcher.Dispatch(new UpdateCommentCommand(request.LogType, request.SubType, request.Comment, request.OperationsAreaId, request.Author, request.Unit, request.EffectiveTime));
            return Ok();
        }
    }
}
