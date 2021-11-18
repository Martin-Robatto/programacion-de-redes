using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Protocol;
using ServerAdmin.Models;
using SettingsLogic;
using SettingsLogic.Interface;

namespace ServerAdmin.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly ISettingsManager _settingsManager = new SettingsManager();
        private readonly GrpcChannel _channel;
        
        public UserController()
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
        public async Task<IActionResult> Post([FromBody] UserModelIn model)
        {
            var client = new UserManager.UserManagerClient(_channel);
            var userLine = new UserParam() {Line = model.Parse()};
            var response = await client.PostUserAsync(userLine);
            UserModelOut modelOut = new UserModelOut()
            {
                Username = model.Username
            };
            return response.StatusCode == StatusCodeConstants.CREATED ? Created(string.Empty, modelOut) : StatusCode(response.StatusCode);
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] UserModelIn model)
        {
            var client = new UserManager.UserManagerClient(_channel);
            var userLine = new UserParam() {Line = model.Parse()};
            var response = await client.DeleteUserAsync(userLine);
            return response.StatusCode == StatusCodeConstants.OK ? NoContent() : StatusCode(response.StatusCode);
        }
        
        [HttpPut]
        public async Task<IActionResult> Update(string username, [FromBody] UserModelIn model)
        {
            var client = new UserManager.UserManagerClient(_channel);
            var userLine = new UserParam() {Line = model.ParseToPutFormat(username)};
            var response = await client.PutUserAsync(userLine);
            return response.StatusCode == StatusCodeConstants.OK ? NoContent() : StatusCode(response.StatusCode);
        }
    }
}