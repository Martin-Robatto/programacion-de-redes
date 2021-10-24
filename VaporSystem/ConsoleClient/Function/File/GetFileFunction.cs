using Protocol;
using SocketLogic;
using System;
using System.Text;

namespace ConsoleClient.Function.File
{
    public class GetFileFunction : FunctionTemplate
    {
        public const string NAME = "Descargar Caratula de Juego";
        private string _gameTitle { get; set; }

        public override DataPacket BuildRequest()
        {
            Console.WriteLine("Ingrese el titulo: ");
            _gameTitle = Console.ReadLine();
            
            var message = $"{base.session}&{_gameTitle}";
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
                base.fileSize = long.Parse(fileAttributes[1]);
                string[] filePathAttributes = fileAttributes[0].Split(".");
                string fileExtension = filePathAttributes[filePathAttributes.Length - 1];
                base.fileName = $@"C:\VAPOR\CLIENT\{_gameTitle}.{fileExtension}";
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