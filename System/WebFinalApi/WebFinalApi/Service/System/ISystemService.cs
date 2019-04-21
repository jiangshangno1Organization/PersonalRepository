using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFinalApi.Empty;

namespace WebFinalApi.Service
{
    public interface ISystemService
    {
        /// <summary>
        /// 获取系统时间
        /// </summary>
        /// <returns></returns>
        DateTime GetSystemTime();


        bool SendVerificationCode(string mobile);

    }
}