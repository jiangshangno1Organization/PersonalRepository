using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFinalApi.Empty
{
    public class OrderCart
    {
        public int ID { get; set; }
        public int userID { get; set; }
        public int gdsID { get; set; }
        public int count { get; set;}
        public DateTime addTime { get; set; }
        public string memo { get; set; }

    }
}