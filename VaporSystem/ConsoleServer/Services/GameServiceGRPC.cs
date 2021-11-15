using System;
using System.Threading.Tasks;
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
            try
            {
                PublishService.Instance.Save(request.Line);
                return Task.FromResult(new GameReply()
                {
                    StatusCode = StatusCodeConstants.CREATED
                });
            }
            catch (AppException exception)
            {
                return Task.FromResult(new GameReply()
                {
                    StatusCode = exception.StatusCode
                });
            }
            catch (Exception)
            {
                return Task.FromResult(new GameReply()
                {
                    StatusCode = StatusCodeConstants.SERVER_ERROR
                });
            }
        }

        public override Task<GameReply> DeleteGame(GameParam request, ServerCallContext context)
        {
            try
            {
                PublishService.Instance.Delete(request.Line);
                return Task.FromResult(new GameReply()
                {
                    StatusCode = StatusCodeConstants.OK
                });
            }
            catch (AppException exception)
            {
                return Task.FromResult(new GameReply()
                {
                    StatusCode = exception.StatusCode
                });
            }
            catch (Exception)
            {
                return Task.FromResult(new GameReply()
                {
                    StatusCode = StatusCodeConstants.SERVER_ERROR
                });
            }
        }
        
        public override Task<GameReply> PutGame(GameParam request, ServerCallContext context)
        {
            try
            {
                PublishService.Instance.Update(request.Line);
                return Task.FromResult(new GameReply()
                {
                    StatusCode = StatusCodeConstants.OK
                });
            }
            catch (AppException exception)
            {
                return Task.FromResult(new GameReply()
                {
                    StatusCode = exception.StatusCode
                });
            }
            catch (Exception)
            {
                return Task.FromResult(new GameReply()
                {
                    StatusCode = StatusCodeConstants.SERVER_ERROR
                });
            }
        }
    }
}