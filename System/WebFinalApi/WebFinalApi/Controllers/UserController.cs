using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebFinalApi.CustomException;
using WebFinalApi.Empty;
using WebFinalApi.Filter;
using WebFinalApi.Models;
using WebFinalApi.Models.User;
using WebFinalApi.Service;

namespace WebFinalApi.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService userService;
        private readonly ISystemService systemService;

        public UserController(IUserService user, ISystemService system)
        {
            userService = user;
            systemService = system;
        }

        #region 帐号登录

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [HttpPut]
        public BaseResponseModel<LoginOutPut> UserLogin(LoginModel loginModel)
        {
            string errMsg = string.Empty;
            LoginOutPut loginOutPut = new LoginOutPut();
            try
            {
                //验证提交数据合法性 
                UserDataLegalityVerification(loginModel);
                loginOutPut = userService.UserLogin(loginModel);
            }
            catch (VerificationException ex)
            {
                loginOutPut.remindMsg = ex.Message;
            }
            catch (OperationException ex)
            {
                errMsg = ex.Message;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return ResponsePack.Responsing(loginOutPut, errMeassage: errMsg);
        }

        /// <summary>
        /// 验证帐号合法性
        /// </summary>
        /// <param name="users"></param>
        private void UserDataLegalityVerification(LoginModel users)
        {
            if (string.IsNullOrWhiteSpace(users.mobile))
            {
                throw new VerificationException("手机号码不可为空.");
            }
        }

        #endregion

        #region 用户信息获取

        /// <summary>
        /// 获取用户中心信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AuthorizationFilter]
        public BaseResponseModel<UserDataOutput> GetUserData()
        {
            var user = userService.GetUser(userDataContent.userID);
            return ResponsePack.Responsing(user);
        }

        /// <summary>
        /// 获取用户收货地址
        /// </summary>
        /// <param name="userAddress"></param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizationFilter]
        public BaseResponseModel<AddressOutput> GetUserAddress()
        {
            var result = userService.GetAddress(userDataContent.userID);
            return ResponsePack.Responsing(result);
        }

        /// <summary>
        /// 新增用户收货地址
        /// </summary>
        /// <param name="userAddress"></param>
        /// <returns></returns>
        [HttpPut]
        [AuthorizationFilter]
        public BaseResponseModel<bool> AddUserAddress(UserAddress userAddress)
        {
            var result = userService.UserAddAddress(userAddress ,userDataContent.userID);
            return ResponsePack.Responsing(result);
        }

        /// <summary>
        /// 修改收货地址
        /// </summary>
        /// <param name="userAddress"></param>
        /// <returns></returns>
        [HttpPost]
        [AuthorizationFilter]
        public BaseResponseModel<bool> ChangeUserAddress(UserAddress userAddress)
        {
            var result = userService.UserUpdateAddress(userAddress, userDataContent.userID);
            return ResponsePack.Responsing(result);
        }

        /// <summary>
        /// 删除收货地址
        /// </summary>
        /// <param name="userAddress"></param>
        /// <returns></returns>
        [HttpDelete]
        [AuthorizationFilter]
        public BaseResponseModel<bool> DeleteUserAddress(int addressID)
        {
            var result = userService.UserDeleteAddress(addressID, userDataContent.userID);
            return ResponsePack.Responsing(result);
        }

        #endregion

    }
}