using FileLogic;
using Protocol;
using SocketLogic;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient.Function.File
{
    public class PostFileFunction : FunctionTemplate
    {
        public const string NAME = "Publicar Caratula de Juego";
        public string _gameTitle { get; set; }
        private readonly FileManager _fileManager = new FileManager();

        public override DataPacket BuildRequest()
        {
            Console.WriteLine("Ingrese el titulo: ");
            _gameTitle = Console.ReadLine();
            
            var filePath = string.Empty;
            while (!_fileManager.FileExists(filePath) || !_fileManager.IsValidExtension(filePath))
            {
                Console.WriteLine("Ingrese la ruta de imagen: ");
                filePath = Console.ReadLine();
            }
            string fileName = _fileManager.GetFileName(filePath);
            long fileSize = _fileManager.GetFileSize(filePath);

            var message = $"{base.session}&{_gameTitle}&{fileName}#{filePath}#{fileSize}";
            var header = new Header(HeaderConstants.REQUEST, FunctionConstants.POST_FILE, message.Length);

            return new DataPacket()
            {
                Header = header,
                Payload = message
            };
        }

        public override async Task SendRequestAsync(DataPacket dataPacket)
        {
            await base.networkManager.SendAsync(base.socket, dataPacket);

            string[] attributes = dataPacket.Payload.Split("&");
            string[] fileAttributes = attributes[2].Split("#");
            string fileName = fileAttributes[0];
            string filePath = fileAttributes[1];
            long fileSize = long.Parse(fileAttributes[2]);

            await base.networkManager.UploadFileAsync(base.socket, fileSize, filePath);
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