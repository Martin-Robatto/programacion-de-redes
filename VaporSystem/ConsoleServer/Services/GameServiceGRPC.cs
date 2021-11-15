using System;
using System.Threading.Tasks;
using Exceptions;
using Grpc.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Service;

namespace ConsoleServer
{
    public class GameServiceGRPC : GameManager.GameManagerBase
    {
        private readonly ILogger<GreeterService> _logger;

        public GameServiceGRPC(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<GameReply> CreateGame(GameAttributes request, ServerCallContext context)
        {
            try
            {
                GameService.Instance.Save(request.Title + "#" + request.Genre + "#" + request.Synopisis);
                return Task.FromResult(new GameReply()
                {
                    StatusCode = 201
                });
            }
            catch (AppException exception)
            {
                return Task.FromResult(new GameReply()
                {
                    StatusCode = exception.StatusCode
                });
            }
            catch (Exception exception)
            {
                return Task.FromResult(new GameReply()
                {
                    StatusCode = 500
                });
            }
        }

        public override Task<GameReply> DeleteGame(GameIdentifier request, ServerCallContext context)
        {
            try
            {
                var game_to_delete = GameService.Instance.Get(request.GameTitle);
                GameService.Instance.Delete(game_to_delete);
                return Task.FromResult(new GameReply()
                {
                    StatusCode = 200
                });
            }
            catch (AppException exception)
            {
                return Task.FromResult(new GameReply()
                {
                    StatusCode = exception.StatusCode
                });
            }
            catch (Exception exception)
            {
                return Task.FromResult(new GameReply()
                {
                    StatusCode = 500
                });
            }
        }
        
        public override Task<GameReply> UpdateGame(UpdateAttributes request, ServerCallContext context)
        {
            try
            {
                var game_to_update = GameService.Instance.Get(request.Title);
                GameService.Instance.Update(request.Session + "&" + request.Title + "&" 
                                            + request.NewTitle + "#" + request.NewGenre + "#" + request.NewSynopisis);
                return Task.FromResult(new GameReply()
                {
                    StatusCode = 200
                });
            }
            catch (AppException exception)
            {
                return Task.FromResult(new GameReply()
                {
                    StatusCode = exception.StatusCode
                });
            }
            catch (Exception exception)
            {
                return Task.FromResult(new GameReply()
                {
                    StatusCode = 500
                });
            }
        }
    }
}