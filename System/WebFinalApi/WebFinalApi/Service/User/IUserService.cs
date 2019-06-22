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

        /// <summary>
        /// 获取用户收货地址
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        AddressOutput GetAddress(int UserID);

        /// <summary>
        /// 使用增加收货地址
        /// </summary>
        /// <param name="userAddress"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        bool UserAddAddress(UserAddress userAddress, int UserID);

        /// <summary>
        /// 用户更新收货地址
        /// </summary>
        /// <param name="userAddress"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        bool UserUpdateAddress(UserAddress userAddress, int UserID);

        /// <summary>
        /// 用户删除收货地址
        /// </summary>
        /// <param name="addressID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        bool UserDeleteAddress(int addressID, int UserID);


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