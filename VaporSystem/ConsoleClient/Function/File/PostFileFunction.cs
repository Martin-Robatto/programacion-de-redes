using FileLogic;
using Protocol;
using SocketLogic;
using System;
using System.Text;

namespace ConsoleClient.Function.File
{
    public class PostFileFunction : FunctionTemplate
    {
        public const string NAME = "Publicar foto";
        public string Title { get; set; }

        public override DataPacket BuildRequest()
        {
            var filePath = string.Empty;
            while (!FileManager.FileExists(filePath) || !FileManager.IsValidExtension(filePath))
            {
                Console.WriteLine("Ingrese la ruta de imagen: ");
                filePath = Console.ReadLine();
            }
            string fileName = FileManager.GetFileName(filePath);
            long fileSize = FileManager.GetFileSize(filePath);

            var message = $"{base.session}&{Title}&{fileName}#{filePath}#{fileSize}";
            var header = new Header(HeaderConstants.REQUEST, FunctionConstants.POST_FILE, message.Length);

            return new DataPacket()
            {
                Header = header,
                Payload = message
            };
        }

        public override void SendRequest(DataPacket dataPacket)
        {
            base.networkManager.Send(base.socket, dataPacket);

            string[] attributes = dataPacket.Payload.Split("&");
            string[] fileAttributes = attributes[2].Split("#");
            string fileName = fileAttributes[0];
            string filePath = fileAttributes[1];
            long fileSize = long.Parse(fileAttributes[2]);

            base.networkManager.UploadFile(base.socket, fileSize, filePath);
        }

        public override void ProcessResponse(byte[] bufferData)
        {
            var statusCode = Int32.Parse(Encoding.UTF8.GetString(bufferData, 0, HeaderConstants.STATUS_CODE_LENGTH));
            var data = Encoding.UTF8.GetString(bufferData, HeaderConstants.STATUS_CODE_LENGTH, bufferData.Length - HeaderConstants.COMMAND_LENGTH - 1);
            if (statusCode == StatusCodeConstants.CREATED)
            {
                Console.WriteLine("Foto publicada exitosamente");
            }
            else
            {
                Console.WriteLine($"{statusCode}: {data}");
            }
        }

        public PostFileFunction()
        {
            base.Name = NAME;
        }
    }
}