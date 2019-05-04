// <summary>
// Description      :
// Author           : zxq
// Create Time      : 2019/4/9 13:27:33
// Revision history : （序号，修改内容，修改人，修改时间）
// 1、  2019/4/9 13:27:33
// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebFinalApi.Filter
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorizationFilter : AuthorizationFilterAttribute
    {
        public authFilterType  authFilter { get; set; }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //获取头部
            //从请求头中获取 Authorization
            string key = actionContext.Request.Headers.Authorization.Scheme;
            if (string.IsNullOrWhiteSpace(key))
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, new { a = "未登录" });
            }
            //int userID = Helper.CodeVerificationHelper.ExplainUserID(key);
            base.OnAuthorization(actionContext);
        }

        public enum authFilterType
        {
             needLogin = 1
        }
    }
  
}
