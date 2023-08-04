using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oplog.Api.Models;
using Oplog.Core.Commands.LogTemplates;
using Oplog.Core.Enums;
using Oplog.Core.Infrastructure;
using Oplog.Core.Queries;
using System.Threading.Tasks;

namespace Oplog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LogTemplateController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly ILogTemplateQueries _logTempleQueries;

        public LogTemplateController(ICommandDispatcher commandDispatcher, ILogTemplateQueries logTempleQueries)
        {
            _commandDispatcher = commandDispatcher;
            _logTempleQueries = logTempleQueries;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateLogTemplateRequest request)
        {
            var result = await _commandDispatcher.Dispatch<CreateLogTemplateCommand, CreateLogTemplateResult>(new CreateLogTemplateCommand(request.Name, request.LogTypeId, request.OperationAreaId, request.Text, request.Author, request.Unit, request.Subtype, request.IsCritical, HttpContext.User.Identity.Name));
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var results = await _logTempleQueries.GetAll();
            return Ok(results);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _commandDispatcher.Dispatch<DeleteLogTemplateCommand, DeleteLogTemplateResult>(new DeleteLogTemplateCommand(id));

            if (result.ResultType == ResultType.NotFound)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}
