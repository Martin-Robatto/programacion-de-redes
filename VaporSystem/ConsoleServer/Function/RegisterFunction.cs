﻿using Protocol;
using Service;
using System;
using System.Text;

namespace ConsoleServer.Function
{
    public class RegisterFunction : FunctionTemplate
    {
        public override ResponseData ProcessRequest(byte[] bufferData)
        {
            ResponseData response = new ResponseData();
            response.Function = FunctionConstants.REGISTER;
            try
            {
                var userLine = Encoding.UTF8.GetString(bufferData);
                UserService.Instance.Register(userLine);
                response.StatusCode = StatusCodeConstants.CREATED;
            }
            catch (Exception exception)
            {
                response.StatusCode = StatusCodeConstants.SERVER_ERROR;
            }
            return response;
        }
    }
}