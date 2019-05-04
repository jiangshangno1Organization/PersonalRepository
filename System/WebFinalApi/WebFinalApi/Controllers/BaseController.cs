using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using WebFinalApi.Models.Common;

namespace WebFinalApi.Controllers
{
    public class BaseController : ApiController
    {
        public UserDataContent userDataContent { get; set; }
        public override Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
        {
            //从请求头中获取 Authorization
            if (controllerContext.Request.Headers.Authorization!=null)
            {
                string key = controllerContext.Request.Headers.Authorization.Scheme;
                if (!string.IsNullOrWhiteSpace(key))
                {
                    userDataContent = new UserDataContent() { userId = Helper.CodeVerificationHelper.ExplainUserID(key) };
                }
            }
            return base.ExecuteAsync(controllerContext, cancellationToken);
        }
    }
}