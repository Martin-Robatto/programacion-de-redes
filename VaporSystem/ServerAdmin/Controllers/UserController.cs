using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Protocol;
using ServerAdmin.Models;

namespace ServerAdmin.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly GrpcChannel channel;
        private readonly UserManager.UserManagerClient client;
        
        public UserController()
        {
            channel = GrpcChannel.ForAddress("https://localhost:5001");
            client = new UserManager.UserManagerClient(channel);
        }
        
        [HttpPost]
        public IActionResult Post([FromBody] UserModelIn model)
        {
            var user = Parser.Parser.UserModelToUserAttributes(model);
            var reply = client.CreateUser(user);
            var model_out = Parser.Parser.UserAttributesToUserModelOut(user);
            return Ok(model_out);
        }
    }
}