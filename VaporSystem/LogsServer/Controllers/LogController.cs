using System.Collections.Generic;
using DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace LogsServer.Controllers
{
    [ApiController]
    [Route("logs")]
    public class LogController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var logs = LogRepository.Logs;
            return Ok(logs);
        }
    }
}