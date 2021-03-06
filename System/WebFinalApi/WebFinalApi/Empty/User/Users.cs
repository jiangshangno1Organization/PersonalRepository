﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFinalApi.Empty
{
    public class Users
    {
        public int userId { get; set; }
        public string userName { get; set; } = "用户";
        public string mobile { get; set; }
        public string password { get; set; }
        public DateTime? updateTime { get; set; }
        public string status { get; set; }
        public string memo { get; set; }
        public string ifdel { get; set; }
        public DateTime? inittime { get; set; }
    }
}