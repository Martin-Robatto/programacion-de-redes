using System;
using System.Text;
using Exceptions;
using Protocol;
using Service;
using SocketLogic;

namespace ConsoleServer.Function
{
    public class GetGameByCategoryFunction : FunctionTemplate
    {
        public override ResponseData ProcessRequest(byte[] bufferData)
        {
            ResponseData response = new ResponseData();
            response.Function = FunctionConstants.GET_GAME_BY_CATEGORY;
            try
            {
                var gameLine = Encoding.UTF8.GetString(bufferData);
                response.Data = GameService.Instance.GetByCategory(gameLine);
                response.StatusCode = StatusCodeConstants.OK;
            }
            catch (AppException exception)
            {
                response.Data = exception.Message;
                response.StatusCode = exception.StatusCode;
            }
            catch (Exception exception)
            {
                response.Data = "Error de servidor";
                response.StatusCode = StatusCodeConstants.SERVER_ERROR;
            }
            return response;
        }

        public override void SendResponse(DataPacket dataPacket)
        {
            NetworkStreamManager.Send(networkStream, dataPacket);
            var games = dataPacket.Payload.Split("&");
            foreach (String game in games)
            {
                var attributes = game.Split("#");
                string filePath = attributes[0];
                long fileSize = long.Parse(attributes[1]);
                if (fileSize > 0)
                {
                    NetworkStreamManager.UploadFile(base.networkStream, fileSize, filePath);
                }
            }
        }
    }
}