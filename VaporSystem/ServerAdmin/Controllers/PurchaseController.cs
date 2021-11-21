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
    [Route("purchases")]
    public class PurchaseController : ControllerBase
    {
        private readonly ISettingsManager _settingsManager = new SettingsManager();
        private readonly GrpcChannel _channel;
        
        public PurchaseController()
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
        public async Task<IActionResult> Post([FromBody] PurchaseModelIn model)
        {
            var client = new PurchaseManager.PurchaseManagerClient(_channel);
            var purchaseLine = new PurchaseParam() {Line = model.Parse()};
            var response = await client.PostPurchaseAsync(purchaseLine);
            PurchaseModelOut modelOut = new PurchaseModelOut()
            {
                Title = model.Title,
                User = model.User
            };
            return response.StatusCode == StatusCodeConstants.CREATED ? Created(string.Empty, modelOut) : StatusCode(response.StatusCode);
        }
        
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] PurchaseModelIn model)
        {
            var client = new PurchaseManager.PurchaseManagerClient(_channel);
            var purchaseLine = new PurchaseParam() {Line = model.Parse()};
            var response = await client.DeletePurchaseAsync(purchaseLine);
            return StatusCode(response.StatusCode);
        }
    }
}