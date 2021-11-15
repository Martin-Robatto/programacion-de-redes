using System;
using System.Threading.Tasks;
using Exceptions;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Service;

namespace ConsoleServer
{
    public class UserServiceGRPC : UserManager.UserManagerBase
    {
        private readonly ILogger<GreeterService> _logger;

        public UserServiceGRPC(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }
        
        public override Task<UserReply> CreateUser(UserAttributes request, ServerCallContext context)
        {
            try
            {
                UserService.Instance.Register(request.UserName + "#" + request.UserPassword);
                return Task.FromResult(new UserReply()
                {
                    StatusCode = 201
                });
            }
            catch (AppException exception)
            {
                return Task.FromResult(new UserReply()
                {
                    StatusCode = exception.StatusCode
                });
            }
            catch (Exception exception)
            {
                return Task.FromResult(new UserReply()
                {
                    StatusCode = 500
                });
            }
        }
    }
}