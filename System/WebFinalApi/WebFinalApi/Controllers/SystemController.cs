using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebFinalApi.Service;

namespace WebFinalApi.Controllers
{
    public class SystemController : ApiController
    {
        private ISystemService SystemService;
        public SystemController(ISystemService service)
        {
            SystemService = service;
        }

        public string GetVerificationCode()
        {
            return "";
        }

    }
}
