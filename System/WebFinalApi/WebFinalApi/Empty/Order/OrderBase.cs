using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFinalApi.Empty
{
    public class OrderBase
    {
        public int ID { get; set; }
        public int userID { get; set; }
        public string status { get; set; }
        public decimal sum { get; set; }
        public DateTime inittime { get; set; }
        public string ifpay { get; set; }

        public DateTime paytime { get; set; }

        public string address1 { get; set; }
        public string address2 { get; set; }
        public string mobile { get; set; }
        public string userName { get; set; }
        public string ifdel { get; set; }
        public string memo { get; set; }
        public decimal factNum { get; set; }
        /// <summary>
        /// 备用总金额
        /// </summary>
        public string sparetotalprice { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime changetime { get; set; }

        public int baseID { get; set; }

        public int goodsnumber { get; set; }

    }
}