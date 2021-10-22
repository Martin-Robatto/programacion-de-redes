using Exceptions;
using Protocol;
using Service;
using System;
using System.Text;

namespace ConsoleServer.Function
{
    public class GetGameByTitleFunction : FunctionTemplate
    {
        public override void ProcessRequest(byte[] bufferData)
        {
            
            base.function = FunctionConstants.GET_GAME_BY_TITLE;
            try
            {
                var gameLine = Encoding.UTF8.GetString(bufferData);
                base.data = GameService.Instance.GetByTitle(gameLine);
                base.statusCode = StatusCodeConstants.OK;
            }
            catch (AppException exception)
            {
                base.data = exception.Message;
                base.statusCode = exception.StatusCode;
            }
            catch (Exception exception)
            {
                base.data = "Error de servidor";
                base.statusCode = StatusCodeConstants.SERVER_ERROR;
            }
            
        }
    }
}