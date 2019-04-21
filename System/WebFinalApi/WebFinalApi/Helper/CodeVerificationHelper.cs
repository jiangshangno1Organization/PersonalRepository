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
        public string GetVerificationCode()
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
        public string GenerateLoginKey(int ID)
        {
            int count = ID.ToString().Length+3;
            // 3 + count
            string key =string.Empty;
            if (count >= 10)
            {
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
    }
}