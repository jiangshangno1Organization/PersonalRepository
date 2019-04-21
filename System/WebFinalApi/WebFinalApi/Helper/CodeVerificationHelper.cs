using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFinalApi.Helper
{
    public class CodeVerificationHelper
    {

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns></returns>
        public static string GetVerificationCode()
        {
            //定义一个随机数
            Random r = new Random();
            string code = string.Empty;
            for (int i = 0; i < 6; i++)
            {
                int cell = r.Next(0, 9);
                code += cell;
            }
            return code;
        }

        /// <summary>
        /// 生成LoginKey 18位
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static string GenerateLoginKey(int ID)
        {
            int count = ID.ToString().Length + 3;
            // 3 + count
            string key = string.Empty;

            switch (count)
            {
                case 10:
                    key += "A";
                    break;
                case 11:
                    key += "B";
                    break;
                case 12:
                    key += "C";
                    break;
                default:
                    key += count.ToString();
                    break;
            }
            key += ID;
            //补全 
            int needCount = 18 - 1 - ID.ToString().Length;
            Random r = new Random();
            for (int i = 0; i < needCount; i++)
            {
                int cell = r.Next(0, 9);
                key += cell;
            }
            return key;
        }

        /// <summary>
        /// 翻译key获取ID
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int ExplainUserID(string key)
        {
            string firstCode = key.Substring(0, 1);
            switch (firstCode)
            {
                case "A":
                    firstCode = "10";
                    break;
                case "B":
                    firstCode = "11";
                    break;
                case "C":
                    firstCode = "12";
                    break;
                default:
                    break;
            }
            int count = Convert.ToInt32(firstCode) - 3;
            int ID = Convert.ToInt32(key.Substring(1, count));
            return ID;
        }
    }
}