using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFinalApi.Empty
{
    public class OrderDetail
    {
        public int ID { get; set; }
        /// <summary>
        /// 订单基础表ID
        /// </summary>
        public int baseID { get; set; }
        public int gdsID { get; set; }
        public string gdsCD { get; set; }
        public string gdsName { get; set; }
        public int count { get; set;}
        /// <summary>
        /// 单价
        /// </summary>
        public decimal unitprice { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        public decimal allprice { get; set; }

    }
}