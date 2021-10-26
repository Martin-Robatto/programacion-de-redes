using Exceptions;
using Protocol;
using Service;
using System;
using System.Text;
using Domain;

namespace ConsoleServer.Function.File
{
    public class PostFileFunction : FunctionTemplate
    {
        public override void ProcessRequest(byte[] bufferData)
        {
            
            base.function = FunctionConstants.POST_FILE;
            try
            {
                var fileLine = Encoding.UTF8.GetString(bufferData);
                Process(fileLine);
                base.statusCode = StatusCodeConstants.CREATED;
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

        private void Process(string fileLine)
        {
            PublishService.Instance.CheckInput(fileLine);
            string[] attributes = fileLine.Split("&");
            string[] fileAttributes = attributes[2].Split("#");
            base.fileSize = long.Parse(fileAttributes[2]);
            string[] filePathAttributes = fileAttributes[1].Split(".");
            string fileExtension = filePathAttributes[filePathAttributes.Length - 1];
            Game game = GameService.Instance.Get(attributes[1]);
            base.fileName = $@"C:\VAPOR\SERVER\{game.Id}.{fileExtension}";
            game.PicturePath = fileName;
        }
    }
}