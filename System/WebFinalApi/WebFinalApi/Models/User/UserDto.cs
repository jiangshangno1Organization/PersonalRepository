
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFinalApi.Models.User
{
    public class UserOperationResult
    {
        public bool ifSuccess { get; set; }

        public string remindMeassage { get; set; }
    }


    public class LoginModel
    {

        public string mobile { get; set; }

        public string code { get; set; }
    }

    public class LoginOutPut
    {
        public bool ifSuccess { get; set; }
        public string key { get; set; }
        public string remindMsg { get; set; }
    }

    public class UserDataOutput
    {
        public string name { get; set; }

        public string mobile { get; set; }

        public int needPayOrderCount { get; set; }

        public int waitRecive { get; set; }

    }

}