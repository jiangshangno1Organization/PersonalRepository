using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebFinalApi.Empty;

namespace WebFinalApi.Controllers
{
    public class UserController : ApiController
    {
        [HttpGet]
        public string GetUserData(int ID)
        {
            return "";
        }

        [HttpPut]
        public string RegiestUser(Users user)
        {
            return "";
        }

        [HttpPost]
        public string ChangeUserData(Users user)
        {
            return "";
        }
    }
}