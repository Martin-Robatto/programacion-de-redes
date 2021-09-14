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
            response.Function = FunctionConstants.GetAllGames;
            try
            {
                response.Data = GameService.Instance.GetGames();
                response.StatusCode = StatusCodeConstants.Ok;
            }
            catch (Exception exception)
            {
                response.StatusCode = StatusCodeConstants.ServerError;
            }
            return response;
        }
    }
}
