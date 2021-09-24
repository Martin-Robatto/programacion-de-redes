﻿using System;
using System.Text;
using Exceptions;
using Protocol;
using Service;

namespace ConsoleServer.Function
{
    public class GetPublishesByUserFunction : FunctionTemplate
    {
        public override ResponseData ProcessRequest(byte[] bufferData)
        {
            ResponseData response = new ResponseData();
            response.Function = FunctionConstants.GET_PUBLISHES_BY_USER;
            try
            {
                var userLine = Encoding.UTF8.GetString(bufferData);
                response.Data = PublishService.Instance.Get(userLine);
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