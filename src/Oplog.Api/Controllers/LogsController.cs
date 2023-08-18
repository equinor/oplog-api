using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oplog.Api.Models;
using Oplog.Core.Commands.Logs;
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
        private readonly IHttpContextAccessor _contextAccessor;
        public LogsController(ICommandDispatcher commandDispatcher, ILogsQueries queries, IHttpContextAccessor contextAccessor)
        {
            _commandDispatcher = commandDispatcher;
            _queries = queries;
            _contextAccessor = contextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _queries.GetAllLogs();
            return Ok(result);
        }

        [HttpPost("filter")]
        public async Task<IActionResult> Post(GetFilteredLogsRequest request)
        {
            var filter = new LogsFilter(request.LogTypeIds, request.AreaIds, request.SubTypeIds, request.UnitIds, request.SearchText, request.FromDate.Value, request.ToDate.Value, request.SortField, request.SortDirection);
            var result = await _queries.GetFilteredLogs(filter);
            return Ok(result);
        }

        [HttpGet("{fromDate}/{toDate}")]
        public async Task<IActionResult> Get(DateTime fromDate, DateTime toDate)
        {
            var result = await _queries.GetLogsByDate(fromDate, toDate);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateLogRequest request)
        {
            var result = await _commandDispatcher.Dispatch<CreateLogCommand, CreateLogResult>(new CreateLogCommand(request.LogType.Value, request.SubType, request.Comment, request.OperationsAreaId.Value, GetFullName(), request.Unit.Value, request.EffectiveTime.Value, GetUserName(), request.IsCritical));
            return Ok(result.Message);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateLogRequest request)
        {
            var result = await _commandDispatcher.Dispatch<UpdateLogCommand, UpdateLogResult>(new UpdateLogCommand(id, request.LogType.Value, request.SubType, request.Comment, request.OperationsAreaId.Value, request.Author, request.Unit.Value, request.EffectiveTime.Value, GetUserName(), request.IsCritical));
            return Ok(result.Message);
        }

        [HttpDelete]
        //TODO: Mark as soft delete
        public async Task<IActionResult> Delete(IEnumerable<int> ids)
        {
            var result = await _commandDispatcher.Dispatch<DeleteLogsCommand, DeleteLogsResult>(new DeleteLogsCommand(ids));
            return Ok(result);
        }

        private string GetFullName()
        {
            var givenName = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.GivenName);
            var surname = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Surname);
            return $"{givenName} {surname}";
        }

        private string GetUserName()
        {
            return _contextAccessor.HttpContext.User.Identity.Name;
        }

    }
}
