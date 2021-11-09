using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DataAccess;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace LogsServer.Controllers
{
    [ApiController]
    [Route("logs")]
    public class LogController : ControllerBase
    {

        private IEnumerable<Log> _logs = new List<Log>();

        [HttpGet]
        public IActionResult Get(string date, string hour, string user, string game)
        {
            _logs = LogRepository.Logs;
            FilterBy(date, log => log.Date.Equals(date));
            FilterBy(hour, log => log.Hour.Equals(hour));
            FilterBy(user, log => log.User.Equals(user));
            FilterBy(game, log => log.Game.Equals(game));
            return Ok(_logs);
        }

        private void FilterBy(string feature, Func<Log, bool> filter)
        {
            if (!string.IsNullOrEmpty(feature))
            {
                _logs = _logs.Where(filter);
            }
        }
        
    }
}