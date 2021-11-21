using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Protocol;
using ServerAdmin.Models;
using SettingsLogic;
using SettingsLogic.Interface;

namespace ServerAdmin.Controllers
{
    [ApiController]
    [Route("games")]
    public class GameController : ControllerBase
    {
        private readonly ISettingsManager _settingsManager = new SettingsManager();
        private readonly GrpcChannel _channel;
        
        public GameController()
        {
            var protocol = _settingsManager.ReadSetting(ServerConfig.ProtocolConfigKey);
            var ip = _settingsManager.ReadSetting(ServerConfig.IPAddressConfigKey);
            var port = _settingsManager.ReadSetting(ServerConfig.GRPCPortConfigKey);
            _channel = GrpcChannel.ForAddress($"{protocol}://{ip}:{port}", new GrpcChannelOptions() {
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
            GameModelOut modelOut = new GameModelOut() {Title = model.Title};
            return response.StatusCode == StatusCodeConstants.CREATED ? Created(string.Empty, modelOut) : StatusCode(response.StatusCode);
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
            GameModelOut modelOut = new GameModelOut() {Title = model.Title};
            return response.StatusCode == StatusCodeConstants.OK ? Created(string.Empty, modelOut) : StatusCode(response.StatusCode);
        }
    }
}