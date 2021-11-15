using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using ServerAdmin.Models;

namespace ServerAdmin.Controllers
{
    [ApiController]
    [Route("games")]
    public class GameController : ControllerBase
    {
        private readonly GrpcChannel channel;
        private readonly GameManager.GameManagerClient client;
        
        public GameController()
        {
            channel = GrpcChannel.ForAddress("https://localhost:5001");
            client = new GameManager.GameManagerClient(channel);
        }
        
        [HttpPost]
        public IActionResult Post([FromBody] GameModelIn model)
        {
            var user = Parser.Parser.GameModelInToGameAttributes(model);
            var reply = client.CreateGameAsync(user);
            var model_out = Parser.Parser.GameAttributesToGameModelOut(user);
            return Ok(model_out);
        }
        
        [HttpDelete]
        public IActionResult Delete([FromRoute] string username)
        {
            return Ok(null);
        }
        
        [HttpPut]
        public IActionResult Update([FromRoute] string username, [FromBody] GameModelIn model)
        {
            return Ok(null);
        }
    }
}