using System;
using System.Threading.Tasks;
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
            try
            {
                UserService.Instance.Register(request.Line);
                return Task.FromResult(new UserReply()
                {
                    StatusCode = StatusCodeConstants.CREATED
                });
            }
            catch (AppException exception)
            {
                return Task.FromResult(new UserReply()
                {
                    StatusCode = exception.StatusCode
                });
            }
            catch (Exception)
            {
                return Task.FromResult(new UserReply()
                {
                    StatusCode = StatusCodeConstants.SERVER_ERROR
                });
            }
        }

        public override Task<UserReply> DeleteUser(UserParam request, ServerCallContext context)
        {
            try
            {
                UserService.Instance.Delete(request.Line);
                return Task.FromResult(new UserReply()
                {
                    StatusCode = StatusCodeConstants.OK
                });
            }
            catch (AppException exception)
            {
                return Task.FromResult(new UserReply()
                {
                    StatusCode = exception.StatusCode
                });
            }
            catch (Exception)
            {
                return Task.FromResult(new UserReply()
                {
                    StatusCode = StatusCodeConstants.SERVER_ERROR
                });
            }
        }
        
        public override Task<UserReply> PutUser(UserParam request, ServerCallContext context)
        {
            try
            {
                UserService.Instance.Update(request.Line);
                return Task.FromResult(new UserReply()
                {
                    StatusCode = StatusCodeConstants.OK
                });
            }
            catch (AppException exception)
            {
                return Task.FromResult(new UserReply()
                {
                    StatusCode = exception.StatusCode
                });
            }
            catch (Exception)
            {
                return Task.FromResult(new UserReply()
                {
                    StatusCode = StatusCodeConstants.SERVER_ERROR
                });
            }
        }
    }
}