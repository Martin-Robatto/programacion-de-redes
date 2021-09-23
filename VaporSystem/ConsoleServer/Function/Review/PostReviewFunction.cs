using System;
using System.Text;
using Exceptions;
using Protocol;
using Service;

namespace ConsoleServer.Function.Review
{
    public class PostReviewFunction : FunctionTemplate
    {
        public override ResponseData ProcessRequest(byte[] bufferData)
        {
            ResponseData response = new ResponseData();
            response.Function = FunctionConstants.POST_REVIEW;
            try
            {
                var reviewLine = Encoding.UTF8.GetString(bufferData);
                ReviewService.Instance.Save(reviewLine);
                response.StatusCode = StatusCodeConstants.CREATED;
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