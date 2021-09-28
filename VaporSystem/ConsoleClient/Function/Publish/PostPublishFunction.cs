﻿using System;
using System.Text;
using ConsoleClient.Function.File;
using FileLogic;
using FunctionInterface;
using Protocol;

namespace ConsoleClient.Function
{
    public class PostPublishFunction : FunctionTemplate
    {
        public const string NAME = "Publicar juego";
        private PostFileFunction _postFileFunction = new PostFileFunction();

        public override DataPacket BuildRequest()
        {
            Console.WriteLine("Ingrese el titulo: ");
            var title = Console.ReadLine();
            Console.WriteLine("Ingrese el genero: ");
            var genre = Console.ReadLine();
            Console.WriteLine("Ingrese la sinopsis: ");
            var synopsis = Console.ReadLine();
            _postFileFunction.Title = title;
                
            var message = $"{base.session}&{title}#{genre}#{synopsis}";
            var header = new Header(HeaderConstants.REQUEST, FunctionConstants.POST_PUBLISH, message.Length);
            
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
            if (statusCode == StatusCodeConstants.CREATED)
            {
                Console.WriteLine("Juego publicado exitosamente");
                Console.WriteLine();
                _postFileFunction.Execute(base.networkStream, base.session);
            }
            else
            {
                Console.WriteLine($"{statusCode}: {data}");
            }
        }

        public PostPublishFunction()
        {
            base.Name = NAME;
        }
    }
}