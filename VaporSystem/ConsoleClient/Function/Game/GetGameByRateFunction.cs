using System;
using System.Text;
using Protocol;

namespace ConsoleClient.Function
{
    public class GetGameByRateFunction : FunctionTemplate
    {
        public const string NAME = "Buscar Juegos por Calificacion";

        public override DataPacket BuildRequest(string session)
        {
            Console.WriteLine("Ingrese la calificacion: ");
            var rate = Console.ReadLine();

            var message = $"{session}&{rate}";
            var header = new Header(HeaderConstants.REQUEST, FunctionConstants.GET_GAME_BY_RATE, message.Length);
            return new DataPacket()
            {
                Header = header,
                Payload = message,
                StatusCode = StatusCodeConstants.EMPTY
            };
        }

        public override void ProcessResponse(byte[] bufferData)
        {
            var statusCode = Int32.Parse(Encoding.UTF8.GetString(bufferData, 0, HeaderConstants.STATUS_CODE_LENGTH));
            var data = Encoding.UTF8.GetString(bufferData, HeaderConstants.STATUS_CODE_LENGTH,
                bufferData.Length - HeaderConstants.COMMAND_LENGTH - 1);
            if (statusCode == StatusCodeConstants.OK)
            {
                var games = data.Split("&");
                Console.WriteLine("Juegos: ");
                foreach (String game in games)
                {
                    var attributes = game.Split("#");
                    Console.WriteLine();
                    Console.WriteLine($"Titulo: {attributes[0]}");
                    Console.WriteLine($"Genero: {attributes[1]}");
                    Console.WriteLine($"Sinopsis: {attributes[2]}");
                    Console.WriteLine($"Calificacion: {attributes[3]}");
                }
            }
            else
            {
                Console.WriteLine($"{statusCode}: {data}");
            }
        }

        public GetGameByRateFunction()
        {
            base.Name = NAME;
        }
    }
}