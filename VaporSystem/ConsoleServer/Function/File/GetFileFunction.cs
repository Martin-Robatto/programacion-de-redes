using System;
using System.Text;
using DataAccess;
using Exceptions;
using Protocol;
using Service;
using SocketLogic;

namespace ConsoleServer.Function.File
{
    public class GetFileFunction : FunctionTemplate
    {
        public override ResponseData ProcessRequest(byte[] bufferData)
        {
            ResponseData response = new ResponseData();
            response.Function = FunctionConstants.GET_FILE;
            try
            {
                var gameLine = Encoding.UTF8.GetString(bufferData);
                response.Data = GameService.Instance.UploadPicture(gameLine);
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
            if (dataPacket.StatusCode == StatusCodeConstants.OK)
            {
                string[] attributes = dataPacket.Payload.Split("#");
                string filePath = attributes[0];
                long fileSize = long.Parse(attributes[1]);
            
                NetworkStreamManager.UploadFile(networkStream, fileSize, filePath);
            }
        }
    }
}