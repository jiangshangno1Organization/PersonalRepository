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
            var user = userService.GetUser(userDataContent.userId);
            return ResponsePack.Responsing(user);
        }

        #endregion

    }
}