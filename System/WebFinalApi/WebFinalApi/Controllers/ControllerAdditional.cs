using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFinalApi.Models;

namespace WebFinalApi.Controllers
{
    public class ControllerAdditional
    {
        public BaseResponseModel<T> ToResponse<T>(T data)
        {
            return new Models.BaseResponseModel<T>() { data = data , requestIfSuccess= true, errMeassage= "" };
        }
    }
}