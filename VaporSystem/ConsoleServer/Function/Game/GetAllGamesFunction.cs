using Exceptions;
using Protocol;
using Service;
using System;

namespace ConsoleServer.Function
{
    public class GetAllGamesFunction : FunctionTemplate
    {
        public override ResponseData ProcessRequest(byte[] bufferData)
        {
            ResponseData response = new ResponseData();
            response.Function = FunctionConstants.GET_ALL_GAMES;
            try
            {
                response.Data = GameService.Instance.GetGames();
                response.StatusCode = StatusCodeConstants.OK;
            }
            catch (AppException exception)
            {
                response.Data = exception.Message;
                response.StatusCode = exception.StatusCode;
            }
            catch (Exception exception)
            {
                response.Data = "Error de servidor";
                response.StatusCode = StatusCodeConstants.SERVER_ERROR;
            }
            return response;
        }
    }
}
