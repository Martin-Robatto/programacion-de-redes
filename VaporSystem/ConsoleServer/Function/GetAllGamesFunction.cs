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
            catch (Exception exception)
            {
                response.StatusCode = StatusCodeConstants.SERVER_ERROR;
            }
            return response;
        }
    }
}
