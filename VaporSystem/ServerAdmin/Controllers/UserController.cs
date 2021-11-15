using System;
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
        private readonly GrpcChannel _channel;
        
        public UserController()
        {
            _channel = GrpcChannel.ForAddress("http://localhost:9000", new GrpcChannelOptions() {
                HttpClient = new System.Net.Http.HttpClient() {
                    DefaultRequestVersion = new Version(2, 0)
                }
            });
        }
        
        [HttpPost]
        public IActionResult Post([FromBody] UserModelIn model)
        {
            var client = new UserManager.UserManagerClient(_channel);
            var userLine = new UserParam() {Line = model.Parse()};
            var response = client.PostUser(userLine);
            return StatusCode(response.StatusCode);
        }
        
        [HttpDelete]
        public IActionResult Delete([FromBody] UserModelIn model)
        {
            var client = new UserManager.UserManagerClient(_channel);
            var userLine = new UserParam() {Line = model.Parse()};
            var response = client.DeleteUser(userLine);
            return StatusCode(response.StatusCode);
        }
        
        [HttpPut]
        public IActionResult Update([FromRoute] string username, [FromBody] UserModelIn model)
        {
            var client = new UserManager.UserManagerClient(_channel);
            var userLine = new UserParam() {Line = model.ParseToPutFormat(username)};
            var response = client.PutUser(userLine);
            return StatusCode(response.StatusCode);
        }
    }
}