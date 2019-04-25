using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebFinalApi.CustomException;
using WebFinalApi.Models;
using WebFinalApi.Service;

namespace WebFinalApi.Controllers
{
    public class SystemController : ApiController
    {
        private readonly ISystemService SystemService;
        public SystemController(ISystemService service)
        {
            SystemService = service;
        }

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public BaseResponseModel<bool> SendVerificationCode(string mobile)
        {
            bool result = false;
            string errMsg = string.Empty;
            try
            {
                result = SystemService.SendVerificationCode(mobile);
            }
            catch (VerificationException ex)
            {
                errMsg = ex.Message;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return ResponsePack.Responsing(result, errMeassage: errMsg);
        }

    }
}
