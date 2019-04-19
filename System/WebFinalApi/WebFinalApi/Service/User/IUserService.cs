using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFinalApi.Empty;

namespace WebFinalApi.Service
{
    public interface IUserService 
    {
        Users GetUser(string ID);

        IEnumerable<Users> GetAllUsers();

        bool UpdateUser(Users user);

        bool DeleteUser(string ID);


        #region 帐号注册

        /// <summary>
        /// 注册执行
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool RegisterUser(Users user);

        /// <summary>
        /// 验证注册数据是否冲突
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool VerificationUser(Users user);

        #endregion

    }
}