using Protocol;
using SocketLogic;
using System;
using System.Text;

namespace ConsoleClient.Function.File
{
    public class GetFileFunction : FunctionTemplate
    {
        public const string NAME = "Obtener foto";
        public string Title { get; set; }

        public override DataPacket BuildRequest()
        {
            var message = $"{base.session}&{Title}";
            var header = new Header(HeaderConstants.REQUEST, FunctionConstants.GET_FILE, message.Length);

            return new DataPacket()
            {
                Header = header,
                Payload = message
            };
        }

        public override void ProcessResponse(byte[] bufferData)
        {
            var statusCode = Int32.Parse(Encoding.UTF8.GetString(bufferData, 0, HeaderConstants.STATUS_CODE_LENGTH));
            var data = Encoding.UTF8.GetString(bufferData, HeaderConstants.STATUS_CODE_LENGTH, bufferData.Length - HeaderConstants.COMMAND_LENGTH - 1);
            if (statusCode == StatusCodeConstants.OK)
            {
                string[] fileAttributes = data.Split("#");
                long fileSize = long.Parse(fileAttributes[1]);
                string[] filePathAttributes = fileAttributes[0].Split(".");
                string fileExtension = filePathAttributes[filePathAttributes.Length - 1];
                string fileName = $@"C:\VAPOR\CLIENT\{Title}.{fileExtension}";

                base.networkManager.DownloadFile(base.socket, fileSize, fileName);
            }
            else
            {
                Console.WriteLine($"{statusCode}: {data}");
            }
        }

        public GetFileFunction()
        {
            base.Name = NAME;
        }
    }
}