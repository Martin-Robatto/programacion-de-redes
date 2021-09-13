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
                var allGames = GameService.Instance.GetGames();
                response.Data = allGames.ToString();
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
