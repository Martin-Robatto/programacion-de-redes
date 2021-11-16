using System;
using System.Text;
using System.Threading.Tasks;
using ConsoleServer.Function;
using Domain;
using Exceptions;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Protocol;
using Service;

namespace ConsoleServer
{
    public class PurchaseServiceGRPC : PurchaseManager.PurchaseManagerBase
    {
        private readonly ILogger<PurchaseServiceGRPC> _logger;

        public PurchaseServiceGRPC(ILogger<PurchaseServiceGRPC> logger)
        {
            _logger = logger;
        }

        public override Task<PurchaseReply> PostPurchase(PurchaseParam request, ServerCallContext context)
        {
            byte[] data = Encoding.UTF8.GetBytes(request.Line);
            FunctionTemplate function = new PostPurchaseFunction();
            function.ProcessRequest(data);
            function.SendLog(data);
            return Task.FromResult(new PurchaseReply()
            {
                StatusCode = function.statusCode
            });
        }

        public override Task<PurchaseReply> DeletePurchase(PurchaseParam request, ServerCallContext context)
        {
            byte[] data = Encoding.UTF8.GetBytes(request.Line);
            FunctionTemplate function = new DeletePurchaseFunction();
            function.ProcessRequest(data);
            function.SendLog(data);
            return Task.FromResult(new PurchaseReply()
            {
                StatusCode = function.statusCode
            });
        }
    }
}