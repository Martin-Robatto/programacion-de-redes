using Exceptions;
using Protocol;
using Service;
using System;
using System.Text;

namespace ConsoleServer.Function.Review
{
    public class DeleteReviewFunction : FunctionTemplate
    {
        public override ResponseData ProcessRequest(byte[] bufferData)
        {
            ResponseData response = new ResponseData();
            response.Function = FunctionConstants.DELETE_REVIEW;
            try
            {
                var reviewLine = Encoding.UTF8.GetString(bufferData);
                ReviewService.Instance.Delete(reviewLine);
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