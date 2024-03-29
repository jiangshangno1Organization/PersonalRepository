﻿// <summary>
// Description      :
// Author           : zxq
// Create Time      : 2019/3/30 14:54:24
// Revision history : （序号，修改内容，修改人，修改时间）
// 1、  2019/3/30 14:54:24
// </summary>

using AFinalDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebFinalApi.CustomException;
using WebFinalApi.Empty;
using WebFinalApi.Helper;

namespace WebFinalApi.Service
{
    public class SystemService : BaseService, ISystemService
    {
        public SystemService(CommonDB dB) : base(dB)
        {
        }

     

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool SendVerificationCode(string mobile)
        {
            //系统时间
            DateTime dtNow = GetSystemTime();
            CodeVerification code = new CodeVerification()
            {
                mobile = mobile,
                failtime = 0,
                initTime = dtNow,
                status = "0",
                type = "0",
                ifSend = "0"
            };

            //获取验证码有效时间
            int effectiveSeconds = int.Parse(GetSystemSet(0).value);

            //获取验证码发送间隔
            int sendIntervalSeconds = int.Parse(GetSystemSet(1).value);

            //获取最后一次发送的 有效（未使用） 验证码
            string sqlCondition = SelectSqlGenerate(code ,new List<string>()
            {
                nameof(code.mobile),
                nameof(code.type),
                nameof(code.status)
            });

            string sql = $"SELECT * FROM code_verification {sqlCondition} ORDER BY initTime DESC";
            CodeVerification lastCode = commonDB.QueryFirstOrDefault<CodeVerification>(sql, code);
        
            if (lastCode != null && !string.IsNullOrWhiteSpace(lastCode.value))
            {
                //判断是否过期
                if (lastCode.initTime.AddSeconds(effectiveSeconds) > dtNow)
                {
                    lastCode.initTime = dtNow;
                    //没过期 更新验证码有效时间
                    sqlCondition = UpdateSqlGenerate(lastCode, new List<string>() { nameof(lastCode.initTime) }, new List<string>() { nameof(lastCode.id) });
                    sql = $"UPDATE code_verification {sqlCondition}";
                    commonDB.Excute(sql, lastCode);
                    return true;
                }
                else
                {
                    lastCode.status = "2";
                    //标记验证码已失效
                    sqlCondition = UpdateSqlGenerate(lastCode, new List<string>() { nameof(lastCode.status) }, new List<string>() { nameof(lastCode.id) });
                    sql = $"UPDATE code_verification {sqlCondition}";
                    commonDB.Excute(sql, lastCode);
                    int needIntervalSeconds = ( lastCode.initTime.AddSeconds(sendIntervalSeconds) - dtNow).Seconds;
                    //判断是否达到发送间隔
                    if (needIntervalSeconds>0)
                    {
                        throw new VerificationException($"请等待{needIntervalSeconds}秒后重新发送验证码");
                    }
                    else
                    {  //不return 继续生成

                    }
                }
            }
            ///生成6位values
            code.value = CodeVerificationHelper.GetVerificationCode();
            sqlCondition = InsertSqlGenerate(code, new List<string>()
            {
                nameof(code.failtime),
                nameof(code.initTime),
                nameof(code.mobile),
                nameof(code.status),
                nameof(code.type),
                nameof(code.value),
                nameof(code.ifSend)
            });
            sql = $@"INSERT INTO code_verification {sqlCondition}";
            return commonDB.Excute(sql, code) == 1;
        }

    }
}
