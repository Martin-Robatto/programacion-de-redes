﻿using System;
using System.Net.Sockets;
using System.Text;
using Exceptions;
using Protocol;
using Service;
using SocketLogic;

namespace ConsoleServer.Function.File
{
    public class PostFileFunction : FunctionTemplate
    {
        public override ResponseData ProcessRequest(byte[] bufferData)
        {
            ResponseData response = new ResponseData();
            response.Function = FunctionConstants.POST_PUBLISH;
            try
            {
                var fileLine = Encoding.UTF8.GetString(bufferData);
                PublishService.Instance.DownloadPicture(base.networkStream, fileLine);
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