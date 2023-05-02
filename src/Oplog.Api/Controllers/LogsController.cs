using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oplog.Api.Models;
using Oplog.Core.Commands;
using Oplog.Core.Infrastructure;
using Oplog.Core.Queries;

namespace Oplog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LogsController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly ILogsQueries _queries;
        public LogsController(ICommandDispatcher commandDispatcher, ILogsQueries queries)
        {
            _commandDispatcher = commandDispatcher;
            _queries = queries;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _queries.GetAllLogs();
            return Ok(result);
        }

        [HttpGet("{fromDate}/{toDate}")]
        public async Task<IActionResult> Get(DateTime fromDate, DateTime toDate)
        {
            var result = await _queries.GetLogsByDate(fromDate, toDate);
            return Ok(result);
        }

        //TODO: do model validation
        [HttpPost]
        public async Task<IActionResult> Post(CreateLogRequest request)
        {
            await _commandDispatcher.Dispatch(new CreateLogCommand(request.LogType, request.SubType, request.Comment, request.OperationsAreaId, request.Author, request.Unit, request.EffectiveTime, GetUserName(), request.IsCritical));
            return Ok();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateLogRequest request)
        {
            await _commandDispatcher.Dispatch(new UpdateLogCommand(id, request.LogType, request.SubType, request.Comment, request.OperationsAreaId, request.Author, request.Unit, request.EffectiveTime, GetUserName(), request.IsCritical));
            return Ok();
        }

        private string GetUserName()
        {
            return HttpContext.User.Identity.Name;
        }
    }
}
