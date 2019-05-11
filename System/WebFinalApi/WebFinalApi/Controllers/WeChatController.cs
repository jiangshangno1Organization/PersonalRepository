using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using WebFinalApi.Helper;

using Senparc;
namespace WebFinalApi.Controllers
{
    public class WeChatController: BaseController
    {
        string token = "zhuyidao";

        [HttpGet]
        [ActionName("weChat")]
        public void Validate(String signature, String timestamp, String nonce, String echostr)
        {
            if (CheckSignature(signature, timestamp, nonce))
            {
                if (!string.IsNullOrEmpty(echostr))
                {
                    NetLog.WriteTextLog($"{signature} {timestamp} {nonce} {echostr} RES : TRUE");
                    HttpContext.Current.Response.Write(echostr);
                }
                else
                {
                    NetLog.WriteTextLog($"{signature} {timestamp} {nonce} {echostr} RES : NULL");
                }
            }
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 验证微信签名
        /// </summary>
        /// * 将token、timestamp、nonce三个参数进行字典序排序
        /// * 将三个参数字符串拼接成一个字符串进行sha1加密
        /// * 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信。
        /// <returns></returns>
        public bool CheckSignature(string signature, string timestamp, string nonce)
        {
            string[] ArrTmp = { token, timestamp, nonce };
            Array.Sort(ArrTmp);     //字典排序
            string tmpStr = string.Join("", ArrTmp);
            tmpStr = Helper.CodeVerificationHelper.SHA1(tmpStr);
            tmpStr = tmpStr.ToLower();
            if (tmpStr == signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



     



    }
}