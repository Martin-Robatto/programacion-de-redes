using Microsoft.AspNetCore.Mvc;
using ServerAdmin.Models;

namespace ServerAdmin.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] UserModelIn model)
        {
            return Ok(null);
        }
        
        [HttpDelete]
        public IActionResult Delete([FromRoute] string username)
        {
            return Ok(null);
        }
        
        [HttpPut]
        public IActionResult Delete([FromRoute] string username, [FromBody] UserModelIn model)
        {
            return Ok(null);
        }
    }
}