using System;
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
        public IActionResult Post([FromBody] PurchaseModelIn model)
        {
            var client = new PurchaseManager.PurchaseManagerClient(_channel);
            var purchaseLine = new PurchaseParam() {Line = model.Parse()};
            var response = client.PostPurchase(purchaseLine);
            return StatusCode(response.StatusCode);
        }
        
        [HttpDelete]
        public IActionResult Delete([FromBody] PurchaseModelIn model)
        {
            var client = new PurchaseManager.PurchaseManagerClient(_channel);
            var purchaseLine = new PurchaseParam() {Line = model.Parse()};
            var response = client.DeletePurchase(purchaseLine);
            return StatusCode(response.StatusCode);
        }
    }
}