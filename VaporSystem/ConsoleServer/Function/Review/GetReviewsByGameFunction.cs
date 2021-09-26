using System;
using System.Text;
using Exceptions;
using Protocol;
using Service;

namespace ConsoleServer.Function.Review
{
    public class GetReviewsByGameFunction : FunctionTemplate
    {
        public override ResponseData ProcessRequest(byte[] bufferData)
        {
            ResponseData response = new ResponseData();
            response.Function = FunctionConstants.GET_REVIEWS_BY_GAME;
            try
            {
                var gameLine = Encoding.UTF8.GetString(bufferData);
                response.Data = ReviewService.Instance.GetByGame(gameLine);
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