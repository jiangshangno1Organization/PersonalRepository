using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFinalApi.Empty
{
    public class Goods
    {
        public int goodsID { get; set; }
        public string goodsName { get; set;}
        public int classID { get; set; }
        public string goodsCD { get; set; }
        public decimal price { get; set; }
        public decimal aPrice { get; set; }
        public string ifdel { get; set; }
        public string text1 { get; set; }
        public string text2 { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        public int stock { get; set; }
        /// <summary>
        /// 库存限制
        /// </summary>
        public string stockLimit { get; set; }
    }
}