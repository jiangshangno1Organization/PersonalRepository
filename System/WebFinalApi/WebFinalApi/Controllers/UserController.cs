using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebFinalApi.CustomException;
using WebFinalApi.Empty;
using WebFinalApi.Models.User;
using WebFinalApi.Service;

namespace WebFinalApi.Controllers
{
    public class UserController : ApiController
    {
        private IUserService userService;

        public UserController(IUserService service)
        {
            userService = service;
        }

        [HttpGet]
        public string GetUserData(int ID)
        {
            return "";
        }

        /// <summary>
        /// 帐号注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        public UserOperationResult RegiestUser(Users user)
        {
            UserOperationResult result = null;
            string meassage = string.Empty;
            bool res = false;
            try
            {
                //验证提交数据合法性 
                UserDataLegalityVerification(user);
                //验证数据冲突

                if (userService.RegisterUser(user))
                {
                    res = true;
                }
                else
                {

                }
            }
            catch (VerificationException ex)
            {
                meassage = ex.Message;
            }
            catch (OperationException ex)
            {
                meassage = ex.Message;
            }
            catch (Exception ex)
            {
                meassage = ex.Message;
            }
            finally
            {
                result = new UserOperationResult()
                {
                    ifSuccess = res,
                    remindMeassage = meassage
                };
            }

            return result;
        }

        [HttpPost]
        public string ChangeUserData(Users user)
        {
            return "";
        }



        #region 帐号

        /// <summary>
        /// 验证帐号合法性
        /// </summary>
        /// <param name="users"></param>
        private void UserDataLegalityVerification(Users users)
        {
            if (string.IsNullOrWhiteSpace(users.mobile))
            {
                throw new VerificationException("手机号码不可为空.");
            }
        }

        #endregion
    }
}