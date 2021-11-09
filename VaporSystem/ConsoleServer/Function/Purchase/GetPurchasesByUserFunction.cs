using Exceptions;
using Protocol;
using Service;
using System;
using System.Text;
using Domain;

namespace ConsoleServer.Function
{
    public class GetPurchasesByUserFunction : FunctionTemplate
    {
        public override void ProcessRequest(byte[] bufferData)
        {
            
            base.function = FunctionConstants.GET_PURCHASES_BY_USER;
            try
            {
                var purchaseLine = Encoding.UTF8.GetString(bufferData);
                base.data = PurchaseService.Instance.Get(purchaseLine);
                base.statusCode = StatusCodeConstants.OK;
            }
            catch (AppException exception)
            {
                base.data = exception.Message;
                base.statusCode = exception.StatusCode;
            }
            catch (Exception)
            {
                base.data = "Error de servidor";
                base.statusCode = StatusCodeConstants.SERVER_ERROR;
            }
            
        }

        public override void SendLog(byte[] bufferData)
        {
            var purchaseLine = Encoding.UTF8.GetString(bufferData);
            Log newLog = new Log()
            {
                Date = DateTime.Now.ToShortDateString(),
                Hour = DateTime.Now.ToString("HH:mm"),
                User = purchaseLine,
                Game = null,
                Action = "Get Purchases By User",
                StatusCode = base.statusCode.ToString()
            };
            LogSender.Instance.SendLog(newLog);
        }
    }
}