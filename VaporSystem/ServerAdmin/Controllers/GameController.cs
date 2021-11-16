using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using ServerAdmin.Models;

namespace ServerAdmin.Controllers
{
    [ApiController]
    [Route("games")]
    public class GameController : ControllerBase
    {
        private readonly GrpcChannel _channel;
        
        public GameController()
        {
            _channel = GrpcChannel.ForAddress("http://localhost:9000", new GrpcChannelOptions() {
                HttpClient = new System.Net.Http.HttpClient() {
                    DefaultRequestVersion = new Version(2, 0)
                }
            });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GameModelIn model)
        {
            var client = new GameManager.GameManagerClient(_channel);
            var gameLine = new GameParam() {Line = model.ParseToPostFormat()};
            var response = await client.PostGameAsync(gameLine);
            return StatusCode(response.StatusCode);
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] GameModelIn model)
        {
            var client = new GameManager.GameManagerClient(_channel);
            var gameLine = new GameParam() {Line = model.ParseToDeleteFormat()};
            var response = await client.DeleteGameAsync(gameLine);
            return StatusCode(response.StatusCode);
        }
        
        [HttpPut]
        public async Task<IActionResult> Update(string title, [FromBody] GameModelIn model)
        {
            var client = new GameManager.GameManagerClient(_channel);
            var gameLine = new GameParam() {Line = model.ParseToPutFormat(title)};
            var response = await client.PutGameAsync(gameLine);
            return StatusCode(response.StatusCode);
        }
    }
}