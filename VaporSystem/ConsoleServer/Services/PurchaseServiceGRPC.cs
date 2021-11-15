using System;
using System.Threading.Tasks;
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
            try
            {
                PurchaseService.Instance.Save(request.Line);
                return Task.FromResult(new PurchaseReply()
                {
                    StatusCode = StatusCodeConstants.CREATED
                });
            }
            catch (AppException exception)
            {
                return Task.FromResult(new PurchaseReply()
                {
                    StatusCode = exception.StatusCode
                });
            }
            catch (Exception)
            {
                return Task.FromResult(new PurchaseReply()
                {
                    StatusCode = StatusCodeConstants.SERVER_ERROR
                });
            }
        }

        public override Task<PurchaseReply> DeletePurchase(PurchaseParam request, ServerCallContext context)
        {
            try
            {
                PurchaseService.Instance.Delete(request.Line);
                return Task.FromResult(new PurchaseReply()
                {
                    StatusCode = StatusCodeConstants.OK
                });
            }
            catch (AppException exception)
            {
                return Task.FromResult(new PurchaseReply()
                {
                    StatusCode = exception.StatusCode
                });
            }
            catch (Exception)
            {
                return Task.FromResult(new PurchaseReply()
                {
                    StatusCode = StatusCodeConstants.SERVER_ERROR
                });
            }
        }
    }
}