using System;
using System.Text;
using System.Threading.Tasks;
using ConsoleServer.Function;
using Domain;
using Exceptions;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Protocol;
using Service;

namespace ConsoleServer
{
    public class UserServiceGRPC : UserManager.UserManagerBase
    {
        private readonly ILogger<UserServiceGRPC> _logger;

        public UserServiceGRPC(ILogger<UserServiceGRPC> logger)
        {
            _logger = logger;
        }

        public override Task<UserReply> PostUser(UserParam request, ServerCallContext context)
        {
            byte[] data = Encoding.UTF8.GetBytes(request.Line);
            FunctionTemplate function = new PostUserFunction();
            function.ProcessRequest(data);
            function.SendLog(data);
            return Task.FromResult(new UserReply()
            {
                StatusCode = function.statusCode
            });
        }

        public override Task<UserReply> DeleteUser(UserParam request, ServerCallContext context)
        {
            byte[] data = Encoding.UTF8.GetBytes(request.Line);
            FunctionTemplate function = new DeleteUserFunction();
            function.ProcessRequest(data);
            function.SendLog(data);
            return Task.FromResult(new UserReply()
            {
                StatusCode = function.statusCode
            });
        }
        
        public override Task<UserReply> PutUser(UserParam request, ServerCallContext context)
        {
            byte[] data = Encoding.UTF8.GetBytes(request.Line);
            FunctionTemplate function = new PutUserFunction();
            function.ProcessRequest(data);
            function.SendLog(data);
            return Task.FromResult(new UserReply()
            {
                StatusCode = function.statusCode
            });
        }
    }
}