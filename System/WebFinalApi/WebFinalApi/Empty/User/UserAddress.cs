using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFinalApi.Empty
{
    public class UserAddress
    {
        public int ID { get; set; }
        public int userID { get; set; }
        public string userName { get; set; }
        public string mobile { get; set; }
        public string address { get; set; }
        public string addressde { get; set; }
        public string ifDefault { get; set; } = "1";
        public string ifdel { get; set; } = "0";
        public DateTime moditime { get; set; } = DateTime.Now;
    }
}