using System;
using System.Threading.Tasks;
using Domain;
using Exceptions;
using Grpc.Core;
using Microsoft.Extensions.Logging;
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

        public override Task<PurchaseReply> CreatePurchase(PurchaseAttributes request, ServerCallContext context)
        {
            try
            {
                PurchaseService.Instance.Save(request.PurchaseGame + "&" + request.PurchaseUser);
                return Task.FromResult(new PurchaseReply()
                {
                    StatusCode = 201
                });
            }
            catch (AppException exception)
            {
                return Task.FromResult(new PurchaseReply()
                {
                    StatusCode = exception.StatusCode
                });
            }
            catch (Exception exception)
            {
                return Task.FromResult(new PurchaseReply()
                {
                    StatusCode = 500
                });
            }
        }

        public override Task<PurchaseReply> DeletePurchase(PurchaseToDelete request, ServerCallContext context)
        {
            try
            {
                var purchase_to_delete = PurchaseService.Instance.Get(request.PurchaseUser);
                PurchaseService.Instance.Delete(purchase_to_delete);
                return Task.FromResult(new PurchaseReply()
                {
                    StatusCode = 200
                });
            }
            catch (AppException exception)
            {
                return Task.FromResult(new PurchaseReply()
                {
                    StatusCode = exception.StatusCode
                });
            }
            catch (Exception exception)
            {
                return Task.FromResult(new PurchaseReply()
                {
                    StatusCode = 500
                });
            }
        }
    }
}