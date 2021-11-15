using Microsoft.AspNetCore.Mvc;
using ServerAdmin.Models;

namespace ServerAdmin.Controllers
{
    [ApiController]
    [Route("purchases")]
    public class PurchaseController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] PurchaseModelIn model)
        {
            return Ok(null);
        }
        
        [HttpDelete]
        public IActionResult Delete([FromRoute] string username)
        {
            return Ok(null);
        }
    }
}