using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
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

        private bool IsHandset(string str_handset)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_handset, @"^[1]+[3,5]+\d{9}");
        }

        /// <summary>
        /// 判断输入的字符串是否是一个合法的手机号
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsMobilePhone(string input)
        {
            Regex regex = new Regex("^1[34578]\\d{9}$");
            return regex.IsMatch(input);
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
                if (!IsMobilePhone(mobile))
                {
                    throw new VerificationException("手机号码不合法.");
                } 
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
