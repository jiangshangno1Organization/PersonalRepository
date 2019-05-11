using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFinalApi.Models
{
    public class AccessTokenInfo
    {
        public static AccessToken accessToken = null;
    }


    public class AccessToken
    {
        /// <summary>
        /// 获取到的凭证
        /// </summary>
        private string tokenName;

        /// <summary>
        /// 凭证有效时间  单位:秒
        /// </summary>
        private int expireSecond;

        public string TokenName { get => tokenName; set => tokenName = value; }

        public int ExpireSecond { get => expireSecond; set => expireSecond = value; }
    }
}