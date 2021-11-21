using System;
using System.Text;
using System.Threading.Tasks;
using ConsoleServer.Function;
using Domain;
using Exceptions;
using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Protocol;
using Service;

namespace ConsoleServer
{
    public class GameServiceGRPC : GameManager.GameManagerBase
    {
        private readonly ILogger<GameServiceGRPC> _logger;

        public GameServiceGRPC(ILogger<GameServiceGRPC> logger)
        {
            _logger = logger;
        }

        public override Task<GameReply> PostGame(GameParam request, ServerCallContext context)
        {
            byte[] data = Encoding.UTF8.GetBytes(request.Line);
            FunctionTemplate function = new PostPublishFunction();
            function.ProcessRequest(data);
            function.SendLog(data);
            return Task.FromResult(new GameReply()
            {
                StatusCode = function.statusCode
            });
        }

        public override Task<GameReply> DeleteGame(GameParam request, ServerCallContext context)
        {
            byte[] data = Encoding.UTF8.GetBytes(request.Line);
            FunctionTemplate function = new DeletePublishFunction();
            function.ProcessRequest(data);
            function.SendLog(data);
            return Task.FromResult(new GameReply()
            {
                StatusCode = function.statusCode
            });
        }
        
        public override Task<GameReply> PutGame(GameParam request, ServerCallContext context)
        {
            byte[] data = Encoding.UTF8.GetBytes(request.Line);
            FunctionTemplate function = new PutPublishFunction();
            function.ProcessRequest(data);
            function.SendLog(data);
            return Task.FromResult(new GameReply()
            {
                StatusCode = function.statusCode
            });
        }
    }
}