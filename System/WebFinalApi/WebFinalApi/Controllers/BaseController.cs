using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using WebFinalApi.Helper;
using WebFinalApi.Models.Common;

namespace WebFinalApi.Controllers
{
    public class BaseController : ApiController
    {
        public UserDataContent userDataContent { get; set; }
        public override Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
        {
            Task<HttpResponseMessage> res = null;
            try
            {
                //从请求头中获取 Authorization
                if (controllerContext.Request.Headers.Authorization != null)
                {
                    string key = controllerContext.Request.Headers.Authorization.Scheme;
                    if (!string.IsNullOrWhiteSpace(key))
                    {
                        userDataContent = new UserDataContent() { userID = Helper.CodeVerificationHelper.ExplainUserID(key) };
                    }
                }
                res = base.ExecuteAsync(controllerContext, cancellationToken);
            }
            catch (Exception ex)
            {
                NetLog.WriteTextLog(" err: " +ex.Message);
            }
            return res;
        }
    }
}