using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFinalApi.Models;

namespace WebFinalApi
{
    public class ResponsePack
    {
        public static BaseResponseModel<T> Responsing<T>(T data, string errMeassage = "")
        {
            return new BaseResponseModel<T>()
            {
                requestIfSuccess = true,
                errMeassage = errMeassage,
                data = data
            };
        }
    }
}