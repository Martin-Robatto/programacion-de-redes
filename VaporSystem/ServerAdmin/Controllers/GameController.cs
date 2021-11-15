using System;
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
        public IActionResult Post([FromBody] GameModelIn model)
        {
            var client = new GameManager.GameManagerClient(_channel);
            var gameLine = new GameParam() {Line = model.ParseToPostFormat()};
            var response = client.PostGame(gameLine);
            return StatusCode(response.StatusCode);
        }
        
        [HttpDelete]
        public IActionResult Delete([FromBody] GameModelIn model)
        {
            var client = new GameManager.GameManagerClient(_channel);
            var gameLine = new GameParam() {Line = model.ParseToDeleteFormat()};
            var response = client.DeleteGame(gameLine);
            return StatusCode(response.StatusCode);
        }
        
        [HttpPut]
        public IActionResult Update([FromRoute] string title, [FromBody] GameModelIn model)
        {
            var client = new GameManager.GameManagerClient(_channel);
            var gameLine = new GameParam() {Line = model.ParseToPutFormat(title)};
            var response = client.PutGame(gameLine);
            return StatusCode(response.StatusCode);
        }
    }
}