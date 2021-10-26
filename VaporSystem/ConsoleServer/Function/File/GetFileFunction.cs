using Exceptions;
using Protocol;
using Service;
using SocketLogic;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleServer.Function.File
{
    public class GetFileFunction : FunctionTemplate
    {
        public override void ProcessRequest(byte[] bufferData)
        {
            
            base.function = FunctionConstants.GET_FILE;
            try
            {
                var gameLine = Encoding.UTF8.GetString(bufferData);
                base.data = GameService.Instance.UploadPicture(gameLine);
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

        public override async Task SendResponseAsync(DataPacket dataPacket)
        {
            await base.networkManager.SendAsync(socket, dataPacket);
            if (dataPacket.StatusCode == StatusCodeConstants.OK)
            {
                string[] attributes = dataPacket.Payload.Split("#");
                string filePath = attributes[0];
                long fileSize = long.Parse(attributes[1]);

                await base.networkManager.UploadFileAsync(socket, fileSize, filePath);
            }
        }
    }
}