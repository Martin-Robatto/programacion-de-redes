using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using ServerAdmin.Models;

namespace ServerAdmin.Controllers
{
    [ApiController]
    [Route("purchases")]
    public class PurchaseController : ControllerBase
    {
        private readonly GrpcChannel _channel;
        
        public PurchaseController()
        {
            _channel = GrpcChannel.ForAddress("http://localhost:9000", new GrpcChannelOptions() {
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
            return StatusCode(response.StatusCode);
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