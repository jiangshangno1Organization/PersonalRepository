using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebFinalApi.CustomException;
using WebFinalApi.Empty;
using WebFinalApi.Models;
using WebFinalApi.Models.User;
using WebFinalApi.Service;

namespace WebFinalApi.Controllers
{
    public class UserController : ApiController
    {
        private IUserService userService;
        private ISystemService systemService;

        public UserController(IUserService  user, ISystemService system)
        {
            userService = user;
            systemService = system;
        }

        [HttpGet]
        public string GetUserData(int ID)
        {
            return "";
        }

        [HttpPost]
        [ActionName(name: "ChangeUserData2")]
        public string ChangeUserData()
        {
            return "";
        }


        [HttpPost]
        [ActionName(name: "ChangeUserData1")]
        public void ssss(Users user)
        {
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
            string remindMsg = string.Empty;
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
                remindMsg = ex.Message;
                loginOutPut.remindMsg = remindMsg;
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
    }
}