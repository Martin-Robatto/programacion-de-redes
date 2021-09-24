﻿using System;
using System.Text;
using Exceptions;
using Protocol;
using Service;

namespace ConsoleServer.Function
{
    public class PostPurchaseFunction : FunctionTemplate
    {
        public override ResponseData ProcessRequest(byte[] bufferData)
        {
            ResponseData response = new ResponseData();
            response.Function = FunctionConstants.POST_PURCHASE;
            try
            {
                var purchaseLine = Encoding.UTF8.GetString(bufferData);
                PurchaseService.Instance.Save(purchaseLine);
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