﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oplog.Core.Queries;
using System.Threading.Tasks;

namespace Oplog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OperationAreasController : ControllerBase
    {
        private readonly IOperationAreasQueries _areasQueries;
        public OperationAreasController(IOperationAreasQueries areasQueries)
        {
            _areasQueries = areasQueries;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var results = await _areasQueries.GetAllAreas();

            if (results == null)
            {
                return NotFound();
            }
            return Ok(results);
        }
    }
}