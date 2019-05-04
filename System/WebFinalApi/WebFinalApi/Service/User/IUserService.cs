using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebFinalApi.Empty;
using WebFinalApi.Models.User;

namespace WebFinalApi.Service
{
    public interface IUserService 
    {

        UserDataOutput GetUser(int userID);

        IEnumerable<Users> GetAllUsers();

        bool UpdateUser(Users user);

        bool DeleteUser(string ID);


        #region 帐号登录

        /// <summary>
        /// 注册执行
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool RegisterUser(Users user);

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        LoginOutPut UserLogin(LoginModel loginModel);
        #endregion

    }
}