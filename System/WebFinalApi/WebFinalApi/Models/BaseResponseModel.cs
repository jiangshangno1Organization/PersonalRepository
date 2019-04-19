using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFinalApi.Models
{
    public class BaseResponseModel<T>
    {
        public bool requestIfSuccess { get; set; }

        public string errMeassage { get; set; }
        public T data { get; set; }
    }
}